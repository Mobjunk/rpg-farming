using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionManager : MonoBehaviour, IInteraction
{
    public Player Player => Player.Instance();
    private CursorManager _cursor => CursorManager.Instance();
    
    public virtual void OnInteraction(CharacterManager pCharacterManager) { }

    public virtual void OnSecondaryInteraction(CharacterManager pCharacterManager) { }

    private void OnMouseOver()
    {
        if (_cursor.IsPointerOverUIElement())
        {
            _cursor.SetDefaultCursor();
            return;
        }
        
        Player.CharacterPlaceObject.CurrentGameObjectHoverd = gameObject;
        if (Player.CharacterInteractionManager.GetInteractables().Contains(this))
        {
            _cursor.SetUsableInteractionCursor();
            Player.CharacterInteractionManager.Interactable = this;
        }
        else _cursor.SetNonUsableInteractionCursor();
    }

    private void OnMouseExit()
    {
        _cursor.SetDefaultCursor();
        Player.CharacterPlaceObject.CurrentGameObjectHoverd = null;
        Player.CharacterInteractionManager.Interactable = null;
    }
}
