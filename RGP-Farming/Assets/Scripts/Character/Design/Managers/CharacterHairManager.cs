using UnityEngine;

public class CharacterHairManager : CharacterBodyPartManager
{
    [SerializeField] private Hair characterHair;

    public override void Awake()
    {
        CurrentBodyPart = characterHair;
        base.Awake();
    }
}
