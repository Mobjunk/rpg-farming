using UnityEngine;

public class TreeInteractionAction : CharacterAction
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();

    private Tree _interactedTree;
    
    public TreeInteractionAction(CharacterManager pCharacterManager, Tree pInteractedTree) : base(pCharacterManager)
    {
        _interactedTree = pInteractedTree;
        Utility.SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "axe_swing", true, true);
        TimeRequired = Utility.GetAnimationClipTime(pCharacterManager.CharacterStateManager.GetAnimator(), "axe_swing");
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    public override void OnEndReached()
    {
        AbstractToolItem tool = (AbstractToolItem) _itemBarManager.GetItemSelected();
        _interactedTree.HealthManager.TakeDamage(tool.toolDamage);
        if (_interactedTree.TreeAnimators != null)
        {
            if (_interactedTree.TreeTop != null) _interactedTree.TreeAnimators[1].SetBool("treeHit", true);
            else _interactedTree.TreeAnimators[0].SetBool("hit", true);
        }
        if (_interactedTree.HealthManager.CurrentHealth < (_interactedTree.HealthManager.MaxHealth / 2) && _interactedTree.TreeTop != null)
            _interactedTree.TreeAnimators[1].SetBool("treeFalling", true);
            
        CharacterManager.SetAction(null);
    }
}