using UnityEngine;
using static Utility;
public class CharacterAttackAction : CharacterAction
{
    private SoundManager _soundManager => SoundManager.Instance();
    private Vector2 _attackPosition;
    private Vector2 _hitBox;
    private LayerMask _layerMask;
    private bool _damageDelivered;
    
    public CharacterAttackAction(CharacterManager pCharacterManager, Vector2 pAttackPosition, Vector2 pHitbox, LayerMask pLayerMask) : base(pCharacterManager)
    {
        _attackPosition = pAttackPosition;
        _hitBox = pHitbox;
        _layerMask = pLayerMask;
        _soundManager.ExecuteSound("player_attack");
        _soundManager.ExecuteSound("sword_swing");
        TimeRequired = GetAnimationClipTime(pCharacterManager.CharacterStateManager.GetAnimator(), "sword_swing");
        SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "sword_swing", true, true);
    }

    public override void Update()
    {
        base.Update();
        if (AnimationTime >= TimeRequired / 2 && !_damageDelivered)
        {
            AbstractToolItem tool = (AbstractToolItem) ItemBarManager.Instance().GetItemSelected();
            Collider2D[] enemies = Physics2D.OverlapBoxAll(_attackPosition, _hitBox, 0, _layerMask);

            Debug.Log("enemies.Length: " + enemies.Length);

            foreach (Collider2D enemy in enemies)
            {
                Debug.Log("tool.toolDamage: " + tool.toolDamage + ", " + enemy.name);
                enemy.GetComponentInParent<EnemyHealth>()?.TakeDamage(tool.toolDamage);
            }

            _damageDelivered = true;
        }
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }

    public override void OnEndReached()
    {
        CharacterManager.SetAction(null);
    }
}
