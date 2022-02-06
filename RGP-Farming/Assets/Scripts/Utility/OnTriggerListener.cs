using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class OnTriggerListener : MonoBehaviour
{
    [SerializeField]
    private string[] _allowedTags;

    [SerializeField]
    private UnityEvent _OnTriggerEnter;

    [SerializeField]
    private UnityEvent _OnTriggerExit;

    public void OnTriggerEnter2D(Collider2D pCollision)
    {
        if (HasAllowedTag(pCollision.gameObject))
        {
            _OnTriggerEnter.Invoke();

        }
    }

    public void OnTriggerExit2D(Collider2D pCollision)
    {
        if (HasAllowedTag(pCollision.gameObject))
        {
            _OnTriggerExit.Invoke();
        }
    }

    private bool HasAllowedTag(GameObject pGameObject)
    {
        for (int i = 0; i < _allowedTags.Length; i++)
        {
            if (pGameObject.CompareTag(_allowedTags[i]))
            {
                return true;
            }
        }

        return false;
    }
}
