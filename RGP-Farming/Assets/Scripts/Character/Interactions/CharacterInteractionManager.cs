using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionManager : MonoBehaviour
{
    private CursorManager _cursorManager => CursorManager.Instance();
    
    [SerializeField] private List<InteractionManager> _interactables;

    public List<InteractionManager> GetInteractables()
    {
        return _interactables;
    }

    [SerializeField] private InteractionManager _interactable;

    public InteractionManager Interactable
    {
        get => _interactable;
        set => _interactable = value;
    }

    public void OnCharacterInteraction(CharacterManager pCharacterManager)
    {
        if (_cursorManager.IsPointerOverUIElement() || Interactable == null) return;

        //Set the interactble to null if its no logner in the list
        if (!_interactables.Contains(Interactable))
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
        if (!_interactables.Contains(Interactable))
        {
            Interactable = null;
            return;
        }
        
        Interactable.OnSecondaryInteraction(pCharacterManager);
    }
}
