using UnityEngine;
using static Utility;

public class EnemyDeathAction : CharacterAction
{
    private GameObject _objectToRemove;
    private Animator _animator;
    
    public EnemyDeathAction(CharacterManager pCharacterManager, GameObject pObjectToRemove, Animator pAnimator) : base(pCharacterManager)
    {
        _objectToRemove = pObjectToRemove;
        _animator = pAnimator;
        SetAnimator(_animator, "death", true);
        SoundManager.Instance().ExecuteSound("slime_death");
        TimeRequired = GetAnimationClipTime(_animator, "death");
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    public override bool Interruptable()
    {
        return false;
    }

    public override void OnEndReached()
    {
        GameObject.Destroy(_objectToRemove);
    }
}