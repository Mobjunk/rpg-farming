using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionManager : MonoBehaviour, IInteraction
{
    public virtual void OnInteraction(CharacterManager characterManager) { }

    public virtual void OnSecondaryInteraction(CharacterManager characterManager) { }

    private void OnMouseOver()
    {
        if (Player.Instance().CharacterInteractionManager.GetInteractables().Contains(this))
        {
            CursorManager.Instance().SetUsableInteractionCursor();
            Player.Instance().CharacterInteractionManager.Interactable = this;
        }
        else CursorManager.Instance().SetNonUsableInteractionCursor();
    }

    private void OnMouseExit()
    {
        CursorManager.Instance().SetDefaultCursor();
        Player.Instance().CharacterInteractionManager.Interactable = null;
    }
}
