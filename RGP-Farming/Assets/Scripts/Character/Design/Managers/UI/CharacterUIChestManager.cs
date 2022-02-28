using UnityEngine;

public class CharacterUIChestManager : CharacterUIBodyPartManager<Chest>
{
    [SerializeField] private Chest _characterChest;

    public override void Awake()
    {
        CurrentBodyPart = _characterChest;
        base.Awake();
    }
}