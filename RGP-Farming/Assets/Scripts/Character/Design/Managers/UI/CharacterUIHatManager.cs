using UnityEngine;

public class CharacterUIHatManager : CharacterUIBodyPartManager<Hat>
{
    [SerializeField] private Hat _characterHat;

    public override void Awake()
    {
        CurrentBodyPart = _characterHat;
        base.Awake();
    }
}