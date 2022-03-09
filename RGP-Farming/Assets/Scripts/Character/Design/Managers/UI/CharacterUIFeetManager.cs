using UnityEngine;

public class CharacterUIFeetManager : CharacterUIBodyPartManager<Feet>
{
    [SerializeField] private Feet _characterFeet;

    public override void Awake()
    {
        CurrentBodyPart = _characterFeet;
        base.Awake();
    }
}