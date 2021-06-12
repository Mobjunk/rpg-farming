using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(SortingGroup))]
[RequireComponent(typeof(HeightBasedSorting))]
public class StaticObjectManager : MonoBehaviour
{
    [SerializeField] private InteractionManager interactionManager;
    public InteractionManager InteractionManager
    {
        get => interactionManager;
        set => interactionManager = value;
    }

    private void Awake()
    {
        interactionManager = GetComponent<InteractionManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player characterManager = other.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Add(interactionManager);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player characterManager = other.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Remove(interactionManager);
    }
}
