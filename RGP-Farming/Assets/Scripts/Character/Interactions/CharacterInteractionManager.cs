using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractionManager : MonoBehaviour
{
    private CursorManager cursorManager => CursorManager.Instance();
    
    [SerializeField] private List<InteractionManager> interactables;

    public List<InteractionManager> GetInteractables()
    {
        return interactables;
    }

    [SerializeField] private InteractionManager interactable;

    public InteractionManager Interactable
    {
        get => interactable;
        set => interactable = value;
    }

    public void OnCharacterInteraction(CharacterManager characterManager)
    {
        if (cursorManager.IsPointerOverUIElement()) return;
        
        if (Interactable == null)
        {
            Debug.Log("Interactable is null...");
            return;
        }
        Interactable.OnInteraction(characterManager);
    }

    public void OnCharacterSecondaryInteraction(CharacterManager characterManager)
    {
        if (cursorManager.IsPointerOverUIElement()) return;
        
        if (Interactable == null)
        {
            Debug.Log("Interactable is null...");
            return;
        }
        Interactable.OnSecondaryInteraction(characterManager);
    }
}
