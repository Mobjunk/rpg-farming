using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class StaticObjectManager : MonoBehaviour
{
    [SerializeField] private InteractionManager _interactionManager;
    public InteractionManager InteractionManager
    {
        get => _interactionManager;
        set => _interactionManager = value;
    }

    private void Awake()
    {
        _interactionManager = GetComponent<InteractionManager>();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D pOther)
    {
        Player characterManager = pOther.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Add(_interactionManager);
    }

    private void OnTriggerExit2D(Collider2D pOther)
    {
        Player characterManager = pOther.GetComponent<Player>();
        if (characterManager != null) characterManager.CharacterInteractionManager.GetInteractables().Remove(_interactionManager);
    }
}
