using UnityEngine;

public class CharacterBodyManager : CharacterBodyPartManager
{
    [SerializeField] private Body characterBody;

    public override void Awake()
    {
        CurrentBodyPart = characterBody;
        base.Awake();
    }
}
