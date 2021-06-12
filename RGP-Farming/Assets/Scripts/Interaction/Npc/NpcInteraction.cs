using UnityEngine;

public class NpcInteraction : InteractionManager
{
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Player characterManager = other.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Add(this);
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        Player characterManager = other.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Remove(this);
    }
}