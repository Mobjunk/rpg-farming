using UnityEngine;

public class Npc : CharacterManager
{

    public NpcData NpcData;

    public override void Awake()
    {
        base.Awake();

        if (NpcData != null)
        {

            if (NpcData.randomWalking)
            {
                SetAction(new RandomMovementManager(this));
            }
        }
    }
}
