using UnityEngine;

public class CharacterUIHairManager : CharacterUIBodyPartManager<Hair>
{
    [SerializeField] private Hair _characterHair;

    public override void Awake()
    {
        CurrentBodyPart = _characterHair;
        base.Awake();
    }
}