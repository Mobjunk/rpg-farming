using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterAction
{
    CharacterStates GetCharacterState();

    void Update();

    void OnStart();
    
    void OnStop();

    bool Interruptable();
}