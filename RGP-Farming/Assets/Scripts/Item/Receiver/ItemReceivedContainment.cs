using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemReceivedContainment<T> : MonoBehaviour
{
    [SerializeReference] protected T containment;
    
    public T Containment
    {
        get => containment;
    }
    
    [SerializeField] private Image[] backgrounds;

    public Image[] Backgrounds
    {
        get => backgrounds;
    }

    [SerializeField] private TextMeshProUGUI[] texts;

    public TextMeshProUGUI[] Texts
    {
        get => texts;
    }

    [SerializeField] private Image icon;

    public Image Icon
    {
        get => icon;
    }

    [SerializeField] private TextMeshProUGUI amount;
    
    public TextMeshProUGUI Amount
    {
        get => amount;
    }

    [SerializeField] private TextMeshProUGUI itemName;

    public TextMeshProUGUI ItemName
    {
        get => itemName;
    }
    
    public virtual void SetContainment(T containment)
    {
        this.containment = containment;
    }
    
    public virtual void ClearContainment()
    {
        containment = default;
        amount.text = "";
        icon.enabled = false;
        itemName.text = "";
    }
}
