using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(SortingGroup))]
public class HeightBasedSorting : MonoBehaviour
{
    [SerializeField]
    private SortingGroup _sortingGroup;

    [SerializeField]
    private float _positionScaling = -100;

    [SerializeField] private int _offset;

    [SerializeField] private bool _skip;

    private void OnValidate()
    {
        if (_sortingGroup == null)
            _sortingGroup = GetComponent<SortingGroup>();

        UpdateOrder();
    }

    private void Start()
    {
        UpdateOrder();
    }

    public void UpdateOrder()
    {
        if (_skip) return;
        if (_sortingGroup != null) _sortingGroup.sortingOrder = (int)(transform.position.y * _positionScaling - _offset);
    }
}
