using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopContainment<T> : UIContainerbase<T>
{

    [Header("Shop UI elements")]
    [SerializeField] private Image _mainUISprite;
    public Image MainUISprite => _mainUISprite;
    
    [SerializeField] private TextMeshProUGUI _itemName;
    public TextMeshProUGUI ItemName => _itemName;
    
    [SerializeField] private TextMeshProUGUI _itemNameShadow;
    public TextMeshProUGUI ItemNameShadow => _itemNameShadow;
    
    [SerializeField] private TextMeshProUGUI _itemPrice;
    public TextMeshProUGUI ItemPrice => _itemPrice;
    
    [SerializeField] private Image _goldCoin;
    public Image GoldCoin => _goldCoin;
    
    public override void ClearContainment()
    {
        _containment = default;
        _mainUISprite.enabled = false;
        Icon.enabled = false;
        _itemName.text = "";
        _itemName.text = "";
        _itemPrice.text = "";
        Amount.text = "";
        _goldCoin.enabled = false;
    }

}
