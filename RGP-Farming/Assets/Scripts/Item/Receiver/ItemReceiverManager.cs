using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemReceiverManager : Singleton<ItemReceiverManager>
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private List<ItemReceivedData> itemsReceived = new List<ItemReceivedData>();

    private void Update()
    {
        List<ItemReceivedData> toRemove = new List<ItemReceivedData>();
        foreach (ItemReceivedData data in itemsReceived)
        {
            if (data.timeRemaning > 0) data.timeRemaning -= Time.deltaTime;

            if(data.timeRemaning < 0) toRemove.Add(data);
            else if (data.timeRemaning < 0.625f)
            {
                //Handles updating the opacity for the images
                foreach (Image bg in data.container.Backgrounds)
                {
                    Color color = bg.color;
                    bg.color = new Color(color.r, color.g, color.b, color.a - 0.005f);
                }

                //Handles updating the opacity for all the text
                foreach (TextMeshProUGUI text in data.container.Texts)
                {
                    Color color = text.color;
                    text.color = new Color(color.r, color.g, color.b, color.a - 0.005f);
                }
            }
        }

        //Handles removing all stuff that needs to be removed
        foreach (ItemReceivedData remove in toRemove)
        {
            Destroy(remove.containment);
            itemsReceived.Remove(remove);
        }
    }

    public ItemReceivedData ForItem(Item item)
    {
        return itemsReceived.Where(data => data != null).FirstOrDefault(data => data.container.Containment.item == item.item);
    }

    public void Add(Item item)
    {
        //Check if the item already exists
        ItemReceivedData data = ForItem(item);
        //If it doesnt exist create a new one
        if (data == null)
        {
            GameObject containment = Instantiate(slotPrefab, parent.transform, true);
            containment.transform.localScale = new Vector3(1, 1, 1);

            ItemReceiverContainer container = containment.GetComponent<ItemReceiverContainer>();
            container.SetContainment(item);

            //TODO: Check if the size of itemsReceived is 6
            //TODO: If it does add it to a queue
            //TODO: If the item received is lower then 6 add the first from the queue
            itemsReceived.Add(new ItemReceivedData(containment, container, 2.5f));
        }
        //If it does exist update the amount
        else
        {
            ItemReceiverContainer container = data.container;
            container.SetContainment(new Item(container.Containment.item, container.Containment.amount + item.amount));
            data.timeRemaning = 2.5f;
            data.container.Icon.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

[Serializable]
public class ItemReceivedData
{
    public GameObject containment;
    public ItemReceiverContainer container;
    public float timeRemaning;

    public ItemReceivedData(GameObject containment, ItemReceiverContainer container, float timeRemaning)
    {
        this.containment = containment;
        this.container = container;
        this.timeRemaning = timeRemaning;
    }
}