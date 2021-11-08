using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionManager : MonoBehaviour
{
    private CursorManager _cursorManager => CursorManager.Instance();
    
    public List<InteractionManager> Interactables;

    public InteractionManager Interactable;

    public void OnCharacterInteraction(CharacterManager pCharacterManager)
    {
        if (_cursorManager.IsPointerOverUIElement() || Interactable == null) return;

        //Set the interactble to null if its no logner in the list
        if (!Interactables.Contains(Interactable))
        {
            Interactable = null;
            return;
        }
        
        Interactable.OnInteraction(pCharacterManager);
    }

    public void OnCharacterSecondaryInteraction(CharacterManager pCharacterManager)
    {
        if (_cursorManager.IsPointerOverUIElement() || Interactable == null) return;

        //Set the interactble to null if its no logner in the list
        if (!Interactables.Contains(Interactable))
        {
            Interactable = null;
            return;
        }
        
        Interactable.OnSecondaryInteraction(pCharacterManager);
    }
}
