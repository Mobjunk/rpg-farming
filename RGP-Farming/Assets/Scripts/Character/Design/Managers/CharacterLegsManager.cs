using UnityEngine;

public class CharacterLegsManager : CharacterBodyPartManager
{
    [SerializeField] private Legs characterLegs;

    public override void Awake()
    {
        CurrentBodyPart = characterLegs;
        base.Awake();
    }
}