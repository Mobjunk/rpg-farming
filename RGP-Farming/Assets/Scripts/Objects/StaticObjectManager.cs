using UnityEngine;

public class StaticObjectManager : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField] private InteractionManager _interactionManager;
    
    public InteractionManager InteractionManager
    {
        get => _interactionManager;
        set => _interactionManager = value;
    }

    private void Awake()
    {
        _interactionManager = GetComponent<InteractionManager>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        if(_rigidbody2D != null) _rigidbody2D.bodyType = RigidbodyType2D.Static;
    }

    private void OnTriggerEnter2D(Collider2D pOther)
    {
        Player characterManager = pOther.GetComponent<Player>();
        if (characterManager != null)
        {
            characterManager.CharacterInteractionManager.GetInteractableObjects().Add(gameObject);
            characterManager.CharacterInteractionManager.GetInteractables().Add(_interactionManager);
        }
    }

    private void OnTriggerExit2D(Collider2D pOther)
    {
        Player characterManager = pOther.GetComponent<Player>();
        if (characterManager != null)
        {
            characterManager.CharacterInteractionManager.GetInteractableObjects().Remove(gameObject);
            characterManager.CharacterInteractionManager.GetInteractables().Remove(_interactionManager);
        }
    }
}
