using UnityEngine;

public class CharacterFeetManager : CharacterBodyPartManager
{
    [SerializeField] private Feet _characterFeet;

    public override void Awake()
    {
        CurrentBodyPart = _characterFeet;
        base.Awake();
    }
}