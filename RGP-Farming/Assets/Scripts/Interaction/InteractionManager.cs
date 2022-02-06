using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionManager : MonoBehaviour, IInteraction
{
    private Player _player => Player.Instance();
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
        
        _player.CharacterPlaceObject.CurrentGameObjectHoverd = gameObject;
        if (_player.CharacterInteractionManager.GetInteractables().Contains(this))
        {
            _cursor.SetUsableInteractionCursor();
            _player.CharacterInteractionManager.Interactable = this;
        }
        else _cursor.SetNonUsableInteractionCursor();
    }

    private void OnMouseExit()
    {
        _cursor.SetDefaultCursor();
        _player.CharacterPlaceObject.CurrentGameObjectHoverd = null;
        _player.CharacterInteractionManager.Interactable = null;
    }
}
