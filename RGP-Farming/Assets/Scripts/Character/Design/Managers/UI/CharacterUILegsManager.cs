using UnityEngine;

public class CharacterUILegsManager : CharacterUIBodyPartManager<Legs>
{
    [SerializeField] private Legs _characterLegs;

    public override void Awake()
    {
        CurrentBodyPart = _characterLegs;
        base.Awake();
    }
}