using UnityEngine;

public class NpcPathManager : PathManager
{
    private Npc _npc;
    
    [SerializeField] private Transform _centerPoint;

    public override void Awake()
    {
        base.Awake();
        _npc = GetComponent<Npc>();
        if (_npc.NpcData != null && _npc.NpcData.randomWalking)
            _npc.SetAction(new NewRandomMovemntAction(_npc, _centerPoint, this));
    }
}