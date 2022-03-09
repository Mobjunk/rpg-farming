using UnityEngine;

public class CharacterUIEyesManager : CharacterUIBodyPartManager<Eyes>
{
    [SerializeField] private Eyes _characterEyes;

    public override void Awake()
    {
        CurrentBodyPart = _characterEyes;
        base.Awake();
    }
}