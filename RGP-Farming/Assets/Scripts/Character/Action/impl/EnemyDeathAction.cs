using UnityEngine;
using static Utility;

public class EnemyDeathAction : CharacterAction
{
    private GameObject _objectToRemove;
    private Animator _animator;
    
    public EnemyDeathAction(CharacterManager pCharacterManager, GameObject pObjectToRemove, Animator pAnimator) : base(pCharacterManager)
    {
        Debug.Log("???");
        _objectToRemove = pObjectToRemove;
        _animator = pAnimator;
        SetAnimator(_animator, "death", true);
        TimeRequired = GetAnimationClipTime(_animator, "death");
        Debug.Log("324324");
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
        Debug.Log("???????????");
        GameObject.Destroy(_objectToRemove);
    }
}