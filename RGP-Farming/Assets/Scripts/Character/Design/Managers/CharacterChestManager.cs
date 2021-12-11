using UnityEngine;

public class CharacterChestManager : CharacterBodyPartManager
{
    [SerializeField] private Chest characterChest;

    public override void Awake()
    {
        CurrentBodyPart = characterChest;
        base.Awake();
    }
}