using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemReceiverManager : Singleton<ItemReceiverManager>
{
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _parent;
    [SerializeField] private List<ItemReceivedData> _itemsReceived = new List<ItemReceivedData>();

    private void Update()
    {
        List<ItemReceivedData> toRemove = new List<ItemReceivedData>();
        foreach (ItemReceivedData data in _itemsReceived)
        {
            if (data.TimeRemaning > 0) data.TimeRemaning -= Time.deltaTime;

            if(data.TimeRemaning < 0) toRemove.Add(data);
            else if (data.TimeRemaning < 0.625f)
            {
                //Handles updating the opacity for the images
                foreach (Image bg in data.Container.Backgrounds)
                {
                    Color color = bg.color;
                    bg.color = new Color(color.r, color.g, color.b, color.a - 0.005f);
                }

                //Handles updating the opacity for all the text
                foreach (TextMeshProUGUI text in data.Container.Texts)
                {
                    Color color = text.color;
                    text.color = new Color(color.r, color.g, color.b, color.a - 0.005f);
                }
            }
        }

        //Handles removing all stuff that needs to be removed
        foreach (ItemReceivedData remove in toRemove)
        {
            Destroy(remove.Containment);
            _itemsReceived.Remove(remove);
        }
    }

    public ItemReceivedData ForItem(GameItem pGameItem)
    {
        return _itemsReceived.Where(data => data != null).FirstOrDefault(data => data.Container.Containment.Item == pGameItem.Item);
    }

    public void Add(GameItem pGameItem)
    {
        //Check if the item already exists
        ItemReceivedData data = ForItem(pGameItem);
        //If it doesnt exist create a new one
        if (data == null)
        {
            GameObject containment = Instantiate(_slotPrefab, _parent.transform, true);
            containment.transform.localScale = new Vector3(8, 8, 8);

            ItemReceiverContainer container = containment.GetComponent<ItemReceiverContainer>();
            container.SetContainment(pGameItem);

            //TODO: Check if the size of itemsReceived is 6
            //TODO: If it does add it to a queue
            //TODO: If the item received is lower then 6 add the first from the queue
            _itemsReceived.Add(new ItemReceivedData(containment, container, 2.5f));
        }
        //If it does exist update the amount
        else
        {
            //Handles updating the opacity for the images
            foreach (Image bg in data.Container.Backgrounds)
            {
                Color color = bg.color;
                bg.color = new Color(color.r, color.g, color.b, 1);
            }

            //Handles updating the opacity for all the text
            foreach (TextMeshProUGUI text in data.Container.Texts)
            {
                Color color = text.color;
                text.color = new Color(color.r, color.g, color.b, 1);
            }
            ItemReceiverContainer container = data.Container;
            container.SetContainment(new GameItem(container.Containment.Item, container.Containment.Amount + pGameItem.Amount));
            data.TimeRemaning = 2.5f;
            data.Container.Icon.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

[Serializable]
public class ItemReceivedData
{
    public GameObject Containment;
    public ItemReceiverContainer Container;
    public float TimeRemaning;

    public ItemReceivedData(GameObject pContainment, ItemReceiverContainer pContainer, float pTimeRemaning)
    {
        this.Containment = pContainment;
        this.Container = pContainer;
        this.TimeRemaning = pTimeRemaning;
    }
}