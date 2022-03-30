using UnityEngine;

public class RockInteractionAction : CharacterAction
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();

    private HealthManager _healthManager;
    
    private string objectName;

    private bool _soundPlayed;
    
    public RockInteractionAction(CharacterManager pCharacterManager, HealthManager pHealthManager, string pObjectName = "") : base(pCharacterManager)
    {
        _healthManager = pHealthManager;
        objectName = pObjectName;
        Utility.SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "pickaxe_swing", true, true);
        TimeRequired = Utility.GetAnimationClipTime(pCharacterManager.CharacterStateManager.GetAnimator(), "pickaxe_swing");
    }

    public override void Update()
    {
        base.Update();
        if (AnimationTime >= TimeRequired / 2 && !_soundPlayed)
        {
            SoundManager.Instance().ExecuteSound("SwingTool");
            _soundPlayed = true;
        }
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