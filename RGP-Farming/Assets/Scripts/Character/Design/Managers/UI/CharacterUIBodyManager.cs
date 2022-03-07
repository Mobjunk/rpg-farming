using UnityEngine;

public class CharacterUIBodyManager : CharacterUIBodyPartManager<Body>
{
    [SerializeField] private Body _characterBody;

    public override void Awake()
    {
        CurrentBodyPart = _characterBody;
        base.Awake();
    }
}
