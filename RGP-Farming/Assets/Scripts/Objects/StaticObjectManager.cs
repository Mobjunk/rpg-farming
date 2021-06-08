using System;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(SortingGroup))]
[RequireComponent(typeof(HeightBasedSorting))]
public class StaticObjectManager : MonoBehaviour
{

    [SerializeField] private Collider2D triggerCollider;
    [SerializeField] private InteractionManager interactionManager;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider2D>();
        interactionManager = GetComponent<InteractionManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player characterManager = other.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.Interactable = interactionManager;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player characterManager = other.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.Interactable = null;
    }
}
