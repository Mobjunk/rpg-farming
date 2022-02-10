using UnityEngine;

public class CharacterBeardManager : CharacterBodyPartManager
{
    [SerializeField] private Beard _characterBeard;

    public override void Awake()
    {
        CurrentBodyPart = _characterBeard;
        base.Awake();
    }
}