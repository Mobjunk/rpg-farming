using UnityEngine;

public class CharacterHatManager : CharacterBodyPartManager
{
    [SerializeField] private Hat _characterHat;

    public override void Awake()
    {
        CurrentBodyPart = _characterHat;
        base.Awake();
    }
}