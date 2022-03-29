using UnityEngine;

public class FishermanInteraction : NpcInteraction
{
    private Animator _animator;
    
    public override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();

        Utility.SetAnimator(_animator, "fishing_idle", true);
    }

    public override void HandleOthers()
    {
        
    }
}
