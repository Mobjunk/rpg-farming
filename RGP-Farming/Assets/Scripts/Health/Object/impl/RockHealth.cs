using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHealth : HealthManager
{
    public AbstractItemData resource;
    public override void HandleDeath()
    {
        GroundItemsManager.Instance().Add(new Item(resource), gameObject.transform.position);
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
    }
}
