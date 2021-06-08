using UnityEngine;
using UnityEngine.Rendering;

public class HeightBasedSorting : MonoBehaviour
{
    [SerializeField]
    private SortingGroup sortingGroup;

    [SerializeField]
    private float positionScaling = -100;

    [SerializeField] private int offset;

    [SerializeField] private bool skip;

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
        if (skip) return;
        if (sortingGroup != null)
        {
            sortingGroup.sortingOrder = (int)(transform.position.y * positionScaling - offset);
        }
    }
}
