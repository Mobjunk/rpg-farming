using UnityEngine;

public class CharacterChestManager : CharacterBodyPartManager
{
    [SerializeField] private Chest _characterChest;

    public override void Awake()
    {
        CurrentBodyPart = _characterChest;
        base.Awake();
    }
}