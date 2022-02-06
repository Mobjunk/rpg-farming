using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHealth : HealthManager
{
    public AbstractItemData Resource;
    public override void HandleDeath()
    {
        GroundItemsManager.Instance().Add(new GameItem(Resource), gameObject.transform.position);
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
    }
}
