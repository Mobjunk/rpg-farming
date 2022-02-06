using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemReceivedContainment<T> : MonoBehaviour
{
    [SerializeReference] protected T _containment;
    
    public T Containment
    {
        get => _containment;
    }
    
    [SerializeField] private Image[] _backgrounds;

    public Image[] Backgrounds
    {
        get => _backgrounds;
    }

    [SerializeField] private TextMeshProUGUI[] _texts;

    public TextMeshProUGUI[] Texts
    {
        get => _texts;
    }

    [SerializeField] private Image _icon;

    public Image Icon
    {
        get => _icon;
    }

    [SerializeField] private TextMeshProUGUI _amount;
    
    public TextMeshProUGUI Amount
    {
        get => _amount;
    }

    [SerializeField] private TextMeshProUGUI _itemName;

    public TextMeshProUGUI ItemName
    {
        get => _itemName;
    }
    
    public virtual void SetContainment(T pContainment)
    {
        this._containment = pContainment;
    }
    
    public virtual void ClearContainment()
    {
        _containment = default;
        _amount.text = "";
        _icon.enabled = false;
        _itemName.text = "";
    }
}
