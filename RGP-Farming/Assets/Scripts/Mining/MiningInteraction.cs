using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningInteraction : ObjectInteractionManager
{
    private HealthManager healthManager;
    private ItemBarManager itemBarManager => ItemBarManager.Instance();

    public void Awake()
    {
        healthManager = GetComponent<HealthManager>();
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        if (itemBarManager.IsWearingCorrectTool(ToolType.PICKAXE))
        {
            AbstractToolItem tool = (AbstractToolItem)itemBarManager.GetItemSelected();
            healthManager.TakeDamage(tool.toolDamage);
            Debug.Log($"Tree stump took {tool.toolDamage} damage!");
        }
        else Debug.LogError("Not the correct tool.");
    }
}