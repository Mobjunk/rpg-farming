using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractionManager : MonoBehaviour, IInteraction
{
    private bool isHovering;
    public bool IsHovering => isHovering;
    
    public virtual void OnInteraction(CharacterManager characterManager)
    {
        Debug.Log("Base interaction debug...");
    }
    
    public void OnMouseOver()
    {
        isHovering = Player.Instance().CharacterInteractionManager.Interactable == this;
    }    
    public void OnMouseExit()
    {
        isHovering = false;
    }
    
    
}
