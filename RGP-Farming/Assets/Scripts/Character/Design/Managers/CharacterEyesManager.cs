using UnityEngine;

public class CharacterEyesManager : CharacterBodyPartManager
{
    [SerializeField] private Eyes characterEyes;

    public override void Awake()
    {
        CurrentBodyPart = characterEyes;
        base.Awake();
    }
}
