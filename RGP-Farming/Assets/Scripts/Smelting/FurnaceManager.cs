using System;
using UnityEngine;

public class FurnaceManager : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance();
    private SmeltingManager smeltingManager => SmeltingManager.Instance();
    
    [SerializeField] private ItemContainerGrid fuel;
    [SerializeField] private ItemContainerGrid ore;
    [SerializeField] private ItemContainerGrid bar;
    [SerializeField] private float currentOreTimer, currentCoalTimer;
    private AbstractSmeltingData smeltingData = null;
    private FurnaceInventory furnaceInventory;
    
    private void Awake()
    {
        furnaceInventory = GetComponent<FurnaceInventory>();
    }

    private void Update()
    {

        if (currentCoalTimer <= 0 && HasFuelAndOre())
        {
            fuel.Containment.amount--;
            fuel.UpdateItemContainer();
            currentCoalTimer = 10.25f;
        } else if (currentCoalTimer <= 0 && !HasFuelAndOre() && currentOreTimer != 0)
        {
            currentCoalTimer = 0;
            currentOreTimer = 0;
        }
        if (currentCoalTimer > 0) currentCoalTimer -= Time.deltaTime;
        
        if (!CanSmeltOre()) return;

        currentOreTimer += Time.deltaTime;
        if (currentOreTimer >= smeltingData.smeltTime)
        {
            ore.Containment.amount--;
            if (bar.Containment == null)
                furnaceInventory.Set(2, new Item(smeltingData.completedItem));
            else bar.Containment.amount++;
            
            ore.UpdateItemContainer();
            bar.UpdateItemContainer();
            
            currentOreTimer = 0;
        }
    }

    private bool HasFuelAndOre()
    {
        return fuel.Containment != null && fuel.Containment.item != null && fuel.Containment.item == itemManager.ForName("Coal") && ore.Containment != null && ore.Containment.item != null;
    }

    private bool CanSmeltOre()
    {
        if (fuel == null || ore == null | bar == null) return false;
        
        //Has a item in the fuel slot
        if (currentCoalTimer > 0)
        {
            if (ore.Containment != null && ore.Containment.item != null)
            {
                smeltingData = smeltingManager.GetSmeltingData(ore.Containment.item);
                if (smeltingData == null)
                {
                    Debug.Log("smelting data null: " + ore.Containment.item);
                    return false;
                }

                //Checks if there is nothing in the bar containment or the completed item matches the item in the bar slot
                if (bar.Containment == null || bar.Containment.item == null ||
                    bar.Containment.item == smeltingData.completedItem)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
}
