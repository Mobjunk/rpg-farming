using UnityEngine;

public class BorderManager : MonoBehaviour
{
    [SerializeField] private GameObject _borderTreePrefab;
    [SerializeField] private Vector2 _startingPosition;
    [SerializeField] private bool useXAxis;
    
    private void Awake()
    {
        BoxCollider2D boxCollider2D = GetComponent<BoxCollider2D>();
        for (int index = 0; index < (useXAxis ? boxCollider2D.size.x : boxCollider2D.size.y); index++)
        {
            int addX = useXAxis ? -1 * index : 0;
            int addY = useXAxis ? 0 : -1 * index;
            Instantiate(_borderTreePrefab, new Vector3(transform.position.x + _startingPosition.x - addX, transform.position.y + _startingPosition.y - addY), Quaternion.identity, transform);
        }
    }
}
