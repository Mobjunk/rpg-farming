using UnityEngine;

public class TreeHealth : ObjectHealth
{
    private TestScript _testScript;
    
    public override void Awake()
    {
        base.Awake();
        _testScript = GetComponent<TestScript>();
    }

    public override void HandleDeath()
    {
        base.HandleDeath();
        GroundItemsManager.Instance().Add(new GameItem(ItemManager.Instance().ForName("wood"), 10),gameObject.transform.childCount > 0 ? gameObject.transform.GetChild(0).transform.position : gameObject.transform.position);
        _testScript.UpdateGrid(true);
    }
}