﻿public class TreeHealth : ObjectHealth
{
    public override void HandleDeath()
    {
        base.HandleDeath();
        GroundItemsManager.Instance().Add(new Item(ItemManager.Instance().ForName("wood"), 10),gameObject.transform.GetChild(0).transform.position);
    }
}