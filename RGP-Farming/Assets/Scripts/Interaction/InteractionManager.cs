using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionManager : MonoBehaviour, IInteraction
{
    private Player player => Player.Instance();
    private CursorManager cursor => CursorManager.Instance();
    
    public virtual void OnInteraction(CharacterManager characterManager) { }

    public virtual void OnSecondaryInteraction(CharacterManager characterManager) { }

    private void OnMouseOver()
    {
        player.CharacterPlaceObject.CurrentGameObjectHoverd = gameObject;
        if (player.CharacterInteractionManager.GetInteractables().Contains(this))
        {
            cursor.SetUsableInteractionCursor();
            player.CharacterInteractionManager.Interactable = this;
        }
        else cursor.SetNonUsableInteractionCursor();
    }

    private void OnMouseExit()
    {
        cursor.SetDefaultCursor();
        player.CharacterPlaceObject.CurrentGameObjectHoverd = null;
        player.CharacterInteractionManager.Interactable = null;
    }
}
