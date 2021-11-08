using UnityEngine;

public class CharacterEyesManager : CharacterBodyPartManager
{
    [SerializeField] private Eyes _characterEyes;

    public override void Awake()
    {
        CurrentBodyPart = _characterEyes;
        base.Awake();
    }
}
