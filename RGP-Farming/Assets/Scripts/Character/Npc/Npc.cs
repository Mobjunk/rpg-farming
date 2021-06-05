using UnityEngine;

public class Npc : CharacterManager
{

    [SerializeField] private NpcData npcData;

    public NpcData NpcData
    {
        get => npcData;
        set => npcData = value;
    }

    public override void Awake()
    {
        base.Awake();

        if (npcData != null)
        {

            if (npcData.randomWalking)
            {
                SetAction(new RandomMovementManager(this));
            }
        }
    }
}
