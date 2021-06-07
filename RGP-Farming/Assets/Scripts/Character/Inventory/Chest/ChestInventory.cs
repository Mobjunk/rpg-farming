using System;
using UnityEngine;

public class ChestInventory : AbstractItemInventory
{

    [SerializeField] private ChestInvenotryUIManager chestInventoryUIManager;
    
    public override void Awake()
    {
        base.Awake();

        chestInventoryUIManager = GetComponent<ChestInvenotryUIManager>();
        chestInventoryUIManager.Initialize(this);
    }
}
