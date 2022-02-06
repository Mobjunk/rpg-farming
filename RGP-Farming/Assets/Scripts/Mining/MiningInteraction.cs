using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningInteraction : ObjectInteractionManager
{
    private HealthManager _healthManager;
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();

    public void Awake()
    {
        _healthManager = GetComponent<HealthManager>();
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        if (_itemBarManager.IsWearingCorrectTool(ToolType.PICKAXE))
        {
            AbstractToolItem tool = (AbstractToolItem)_itemBarManager.GetItemSelected();
            _healthManager.TakeDamage(tool.toolDamage);
            Debug.Log($"Tree stump took {tool.toolDamage} damage!");
        }
        else Debug.LogError("Not the correct tool.");
    }
}
