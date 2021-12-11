using UnityEngine;

public class CharacterHatManager : CharacterBodyPartManager
{
    [SerializeField] private Hat characterHat;

    public override void Awake()
    {
        CurrentBodyPart = characterHat;
        base.Awake();
    }
}