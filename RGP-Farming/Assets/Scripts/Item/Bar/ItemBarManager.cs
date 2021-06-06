using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarManager : Singleton<ItemBarManager>
{

    [SerializeField] public int selectedSlot = 0;
    
    private PlayerInvenotryUIManager inventoryUIManager;

    public void Start()
    {
        inventoryUIManager = Player.Instance().GetComponent<PlayerInvenotryUIManager>();
    }

    public void Update()
    {
        if (Player.Instance().ControllerConnected)
        {
            
        }
        else
        {
            //TODO: Add a delay
            UpdateSlot(Input.mouseScrollDelta.y);
        }
    }

    void UpdateSlot(float value)
    {
        if (Input.mouseScrollDelta.y == 0) return;
        
        int nextSlot = Player.Instance().CharacterInventory.GetNextOccupiedSlot(selectedSlot, value > 0);
        if (nextSlot == -1)
        {
            Debug.LogError("No slot to switch to");
            return;
        }

        inventoryUIManager.containers[0][selectedSlot].SetHighlighted(false);
        selectedSlot = nextSlot;
        inventoryUIManager.containers[0][selectedSlot].SetHighlighted(true);
    }
}
