using UnityEngine;
using static Utility;
public class CharacterAttackAction : CharacterAction
{
    private Vector2 _attackPosition;
    private Vector2 _hitBox;
    private LayerMask _layerMask;
    
    public CharacterAttackAction(CharacterManager pCharacterManager, Vector2 pAttackPosition, Vector2 pHitbox, LayerMask pLayerMask) : base(pCharacterManager)
    {
        _attackPosition = pAttackPosition;
        _hitBox = pHitbox;
        _layerMask = pLayerMask;
        TimeRequired = GetAnimationClipTime(pCharacterManager.CharacterStateManager.GetAnimator(), "sword_swing");
        SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "sword_swing", true, true);
    }
    
    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    public override void OnEndReached()
    {
        AbstractToolItem tool = (AbstractToolItem) ItemBarManager.Instance().GetItemSelected();
        Collider2D[] enemies = Physics2D.OverlapBoxAll(_attackPosition, _hitBox, 0, _layerMask);

        foreach (Collider2D enemy in enemies)
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(tool.toolDamage);

        CharacterManager.SetAction(null);
    }
}
