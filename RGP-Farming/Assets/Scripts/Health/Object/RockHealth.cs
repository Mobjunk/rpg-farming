using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHealth : HealthManager
{
    public override void HandleDeath()
    {
        //GroundItemsManager.Instance().Add(new Item(ItemManager.Instance().ForName("wood"), 10), gameObject.transform.position);
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
    }
}
