using System;
using UnityEngine;

public class FurnaceManager : MonoBehaviour
{
    private ItemManager _itemManager => ItemManager.Instance();
    private SmeltingManager _smeltingManager => SmeltingManager.Instance();
    
    [SerializeField] private ItemContainerGrid _fuelContainer;
    [SerializeField] private ItemContainerGrid _oreContainer;
    [SerializeField] private ItemContainerGrid _barContainer;
    [SerializeField] private float _currentOreTimer, currentCoalTimer;
    private AbstractSmeltingData _smeltingData = null;
    private FurnaceInventory _furnaceInventory;
    
    private void Awake()
    {
        _furnaceInventory = GetComponent<FurnaceInventory>();
    }

    private void Update()
    {

        if (currentCoalTimer <= 0 && HasFuelAndOre())
        {
            _fuelContainer.Containment.Amount--;
            _fuelContainer.UpdateItemContainer();
            currentCoalTimer = 10.25f;
        } else if (currentCoalTimer <= 0 && !HasFuelAndOre() && _currentOreTimer != 0)
        {
            currentCoalTimer = 0;
            _currentOreTimer = 0;
        }
        if (currentCoalTimer > 0) currentCoalTimer -= Time.deltaTime;
        
        if (!CanSmeltOre()) return;

        _currentOreTimer += Time.deltaTime;
        if (_currentOreTimer >= _smeltingData.smeltTime)
        {
            _oreContainer.Containment.Amount--;
            if (_barContainer.Containment == null)
                _furnaceInventory.Set(2, new GameItem(_smeltingData.completedItem));
            else _barContainer.Containment.Amount++;
            
            _oreContainer.UpdateItemContainer();
            _barContainer.UpdateItemContainer();
            
            _currentOreTimer = 0;
        }
    }

    private bool HasFuelAndOre()
    {
        return _fuelContainer.Containment != null && _fuelContainer.Containment.Item != null && _fuelContainer.Containment.Item == _itemManager.ForName("Coal") && _oreContainer.Containment != null && _oreContainer.Containment.Item != null;
    }

    private bool CanSmeltOre()
    {
        if (_fuelContainer == null || _oreContainer == null | _barContainer == null) return false;
        
        //Has a item in the fuel slot
        if (currentCoalTimer > 0)
        {
            if (_oreContainer.Containment != null && _oreContainer.Containment.Item != null)
            {
                _smeltingData = _smeltingManager.GetSmeltingData(_oreContainer.Containment.Item);
                if (_smeltingData == null)
                {
                    Debug.Log("smelting data null: " + _oreContainer.Containment.Item);
                    return false;
                }

                //Checks if there is nothing in the bar containment or the completed item matches the item in the bar slot
                if (_barContainer.Containment == null || _barContainer.Containment.Item == null ||
                    _barContainer.Containment.Item == _smeltingData.completedItem)
                {
                    return true;
                }
            }
        }
        return false;
    }
    
}
