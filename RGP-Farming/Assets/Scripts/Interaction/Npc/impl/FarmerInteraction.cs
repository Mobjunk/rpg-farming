using UnityEngine;

public class FarmerInteraction : NpcInteraction
{
    private Animator _animator;
    
    public override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
        
        Utility.SetAnimator(_animator, "hoe", true);
        Utility.SetAnimator(_animator, "moveX", -1f);
    }

    public override void HandleOthers() { }
}