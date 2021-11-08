using UnityEngine;

public class CharacterHairManager : CharacterBodyPartManager
{
    [SerializeField] private Hair _characterHair;

    public override void Awake()
    {
        CurrentBodyPart = _characterHair;
        base.Awake();
    }
}
