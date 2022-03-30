using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHealth : HealthManager
{
    [SerializeField] public AbstractItemData _resource;

    private TestScript _testScript;

    public override void Awake()
    {
        base.Awake();
        _testScript = GetComponent<TestScript>();
    }

    public override void HandleDeath()
    {
        SoundManager.Instance().ExecuteSound("MiningBreakSound");
        GroundItemsManager.Instance().Add(new GameItem(_resource, 1 + Random.Range(0, 4)), gameObject.transform.position);
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
        _testScript.UpdateGrid(true);
    }
}
