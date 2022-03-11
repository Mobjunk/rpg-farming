using UnityEngine;

public class TreeInteractionAction : CharacterAction
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    
    private float _animationTime;
    private float _requiredTime;

    private Tree _interactedTree;
    
    public TreeInteractionAction(CharacterManager pCharacterManager, Tree pInteractedTree) : base(pCharacterManager)
    {
        _interactedTree = pInteractedTree;
        Utility.SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "axe_swing", true, true);
        _requiredTime = Utility.GetAnimationClipTime(pCharacterManager.CharacterStateManager.GetAnimator(), "axe_swing");
    }

    public override void Update()
    {
        base.Update();
        _animationTime += Time.deltaTime;
        if (_animationTime > _requiredTime)
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

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }
    
    /*Utility.SetAnimator(characterManager.CharacterStateManager.GetAnimator(), "axe_swing", true, true);
    AbstractToolItem tool = (AbstractToolItem) itemBarManager.GetItemSelected();
    healthManager.TakeDamage(tool.toolDamage);
    if (_animators != null)
    {
        if (_treeTop != null) _animators[1].SetBool("treeHit", true);
        else _animators[0].SetBool("hit", true);
    }

    if (healthManager.CurrentHealth < (healthManager.MaxHealth / 2) && _treeTop != null)
    _animators[1].SetBool("treeFalling", true);
    Debug.Log($"Tree stump took {tool.toolDamage} damage!");*/
}