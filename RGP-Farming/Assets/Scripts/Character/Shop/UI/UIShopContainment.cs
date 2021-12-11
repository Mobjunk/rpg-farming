using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopContainment<T> : UIContainerbase<T>
{

    [Header("Shop UI elements")]
    [SerializeField] private Image mainUISprite;
    public Image MainUISprite => mainUISprite;
    
    [SerializeField] private TextMeshProUGUI itemName;
    public TextMeshProUGUI ItemName => itemName;
    
    [SerializeField] private TextMeshProUGUI itemNameShadow;
    public TextMeshProUGUI ItemNameShadow => itemNameShadow;
    
    [SerializeField] private TextMeshProUGUI itemPrice;
    public TextMeshProUGUI ItemPrice => itemPrice;
    
    [SerializeField] private Image goldCoin;
    public Image GoldCoin => goldCoin;
    
    public override void ClearContainment()
    {
        containment = default;
        mainUISprite.enabled = false;
        Icon.enabled = false;
        itemName.text = "";
        itemName.text = "";
        itemPrice.text = "";
        Amount.text = "";
        goldCoin.enabled = false;
    }

}
