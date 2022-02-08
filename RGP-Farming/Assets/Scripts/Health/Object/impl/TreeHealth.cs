using UnityEngine;

public class TreeHealth : ObjectHealth
{
    public override void HandleDeath()
    {
        base.HandleDeath();
        GroundItemsManager.Instance().Add(new GameItem(ItemManager.Instance().ForName("wood"), 10),gameObject.transform.childCount > 0 ? gameObject.transform.GetChild(0).transform.position : gameObject.transform.position);
    }
}