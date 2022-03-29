using UnityEngine;

public class RockInteractionAction : CharacterAction
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();

    private HealthManager _healthManager;
    
    private string objectName;
    
    public RockInteractionAction(CharacterManager pCharacterManager, HealthManager pHealthManager, string pObjectName = "") : base(pCharacterManager)
    {
        _healthManager = pHealthManager;
        objectName = pObjectName;
        Utility.SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "pickaxe_swing", true, true);
        TimeRequired = Utility.GetAnimationClipTime(pCharacterManager.CharacterStateManager.GetAnimator(), "pickaxe_swing");
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    public override void OnEndReached()
    {
        AbstractToolItem tool = (AbstractToolItem)_itemBarManager.GetItemSelected();
        _healthManager.TakeDamage(tool.toolDamage);
        Debug.Log($"{objectName} took {tool.toolDamage} damage!");
        CharacterManager.SetAction(null);
    }
}