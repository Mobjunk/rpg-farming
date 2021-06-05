using UnityEngine;
using UnityEngine.Rendering;

public class HeightBasedSorting : MonoBehaviour
{
    [SerializeField]
    private SortingGroup sortingGroup;

    [SerializeField]
    private float positionScaling = -100;

    private void OnValidate()
    {
        if (sortingGroup == null)
        {
            sortingGroup = GetComponent<SortingGroup>();
        }

        UpdateOrder();
    }

    private void Start()
    {
        UpdateOrder();
    }

    public void UpdateOrder()
    {
        if (sortingGroup != null)
        {
            sortingGroup.sortingOrder = (int)(transform.position.y * positionScaling);
        }
    }
}
