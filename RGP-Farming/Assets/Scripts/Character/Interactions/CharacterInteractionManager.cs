using UnityEngine;

public class CharacterInteractionManager : MonoBehaviour
{
    [SerializeField] private InteractionManager interactable;

    public InteractionManager Interactable
    {
        get => interactable;
        set => interactable = value;
    }

    public void OnCharacterInteraction(CharacterManager characterManager)
    {
        if (Interactable == null)
        {
            Debug.Log("Interactable is null...");
            return;
        }
        Interactable.OnInteraction(characterManager);
    }
}
