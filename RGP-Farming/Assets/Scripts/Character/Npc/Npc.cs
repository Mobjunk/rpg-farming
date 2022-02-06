using UnityEngine;

public class Npc : CharacterManager
{

    [SerializeField] private NpcData _npcData;

    public NpcData NpcData
    {
        get => _npcData;
        set => _npcData = value;
    }

    public override void Awake()
    {
        base.Awake();

        if (_npcData != null)
        {

            if (_npcData.randomWalking)
            {
                SetAction(new RandomMovementManager(this));
            }
        }
    }
}
