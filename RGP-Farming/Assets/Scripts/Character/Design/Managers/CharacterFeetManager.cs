using UnityEngine;

public class CharacterFeetManager : CharacterBodyPartManager
{
    [SerializeField] private Feet characterFeet;

    public override void Awake()
    {
        CurrentBodyPart = characterFeet;
        base.Awake();
    }
}