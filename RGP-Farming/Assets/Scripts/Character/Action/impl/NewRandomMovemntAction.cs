using UnityEngine;

public class NewRandomMovemntAction : CharacterAction
{
    private NpcPathManager _npcPath;
    private Npc _npc;
    private Transform _centerPoint;
    private float _movementTime;
    private Vector3 _startPosition;
    
    public NewRandomMovemntAction(CharacterManager pCharacterManager, Transform pCenterPoint, NpcPathManager pNpcPath) : base(pCharacterManager)
    {
        _npc = pCharacterManager as Npc;
        _startPosition = _npc.transform.position;
        _centerPoint = pCenterPoint;
        _npcPath = pNpcPath;
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }
    
    public override void Update()
    {
        base.Update();

        if (_npc.IsBusy) return;

        if (_movementTime > 0 && (_npcPath.Path == null || _npcPath.Path.Length <= 0))
            _movementTime -= Time.deltaTime;
        else if (_movementTime <= 0 && (_npcPath.Path == null || _npcPath.Path.Length <= 0))
        {
            Vector3Int posA = _npcPath.PathfinderManager.GetWorldToCell(_centerPoint.position),
            posB = _npcPath.PathfinderManager.GetWorldToCell(new Vector3(_startPosition.x + Random.Range(-_npc.NpcData.walkingRandius, _npc.NpcData.walkingRandius), _startPosition.y + Random.Range(-_npc.NpcData.walkingRandius, _npc.NpcData.walkingRandius)));
            Vector2[] path = _npcPath.PathfinderManager.FindPath(posA, posB);

            bool succesful = path.Length > 0;
            _npcPath.OnPathFound(path, succesful);
            _movementTime = succesful ? 1 + Random.Range(0, 4) : 0.1f + Random.Range(0, 0.15f);
        }
    }
}