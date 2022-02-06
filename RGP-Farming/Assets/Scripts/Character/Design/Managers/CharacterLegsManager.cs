using UnityEngine;

public class CharacterLegsManager : CharacterBodyPartManager
{
    [SerializeField] private Legs _characterLegs;

    public override void Awake()
    {
        CurrentBodyPart = _characterLegs;
        base.Awake();
    }
}