using UnityEngine;

public class CharacterBodyManager : CharacterBodyPartManager
{
    [SerializeField] private Body _characterBody;

    public override void Awake()
    {
        CurrentBodyPart = _characterBody;
        base.Awake();
    }
}
