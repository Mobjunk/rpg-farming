using UnityEngine;

public delegate void CharacterInputAction();
public delegate void CharacterInputActionAttack(CharacterManager pCharacterManager);
public delegate void CharacterInputActionMove(Vector2 pDirection);
public delegate void CharacterInteraction(CharacterManager pCharacterManager);
public delegate void CharacterSecondaryInteraction(CharacterManager pCharacterManager);

public interface ICharacterInput {
    event CharacterInputActionAttack OnCharacterAttack;
    event CharacterInputActionMove OnCharacterMovement;
    event CharacterInteraction OnCharacterInteraction;
    event CharacterSecondaryInteraction OnCharacterSecondaryInteraction;
}