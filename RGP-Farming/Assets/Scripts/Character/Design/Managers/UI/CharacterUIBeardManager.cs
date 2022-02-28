using UnityEngine;

public class CharacterUIBeardManager : CharacterUIBodyPartManager<Beard>
{
    [SerializeField] private Beard _characterBeard;

    public override void Awake()
    {
        CurrentBodyPart = _characterBeard;
        base.Awake();
    }
}