using UnityEngine;

public delegate void CharacterInputAction();
public delegate void CharacterInputActionMove(Vector2 direction);

public interface ICharacterInput {
    event CharacterInputAction OnCharacterAttack;
    event CharacterInputActionMove OnCharacterMovement;
    event CharacterInputAction OnCharacterInteraction;
}