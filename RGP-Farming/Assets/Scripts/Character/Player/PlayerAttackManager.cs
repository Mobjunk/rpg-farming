using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private Transform _attackPoint;
    private Vector3 _hitBox;

    [SerializeField] private LayerMask _maskOfEnemies;
    private CursorManager _cursorManager => CursorManager.Instance();
    
    public void Attack(CharacterManager pCharacterManager)
    {
        if (_cursorManager.IsPointerOverUIElement()) return;

        int direction = pCharacterManager.CharacterStateManager.GetDirection();
        _attackPoint = GetAttackPoint(((Player)pCharacterManager), direction);
        _hitBox = GetHitbox(direction);
        
        if (_attackPoint != null && _hitBox != null)
        {
            if (pCharacterManager.CharacterAction is CharacterAttackAction) return;
            
            pCharacterManager.SetAction(new CharacterAttackAction(pCharacterManager, _attackPoint.position, _hitBox, _maskOfEnemies));
        }
    }

    private Transform GetAttackPoint(Player pPlayer, int pDirection)
    {
        switch (pDirection)
        {
            case 0: //Down
                return pPlayer.AttackPoints[0];
            case 1: //Left
                return pPlayer.AttackPoints[3];
            case 2: //Right
                return pPlayer.AttackPoints[1];
            case 3: //Up
                return pPlayer.AttackPoints[2];
        }
        return null;
    }

    private Vector3 GetHitbox(int pDirection)
    {
        switch (pDirection)
        {
            case 0: //Down
            case 3: //Up
                return new Vector3(1, 0.75f);
            case 1: //Left
            case 2: //Right
                return new Vector3(0.75f, 1);
        }
        return Vector3.zero;
    }

    private void OnDrawGizmos() //OnDrawGizmosSelected
    {
        if (_attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(_attackPoint.position, _hitBox);
        }
    }
}
