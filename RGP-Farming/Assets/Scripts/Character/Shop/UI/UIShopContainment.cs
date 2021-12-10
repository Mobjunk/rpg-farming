using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShopContainment<T> : UIContainerbase<T>
{

    [Header("Shop UI elements")]
    public Image MainUISprite;
    
    public TextMeshProUGUI ItemName;
    
    public TextMeshProUGUI ItemNameShadow;
    
    public TextMeshProUGUI ItemPrice;
    
    public Image GoldCoin;
    
    public override void ClearContainment()
    {
        Containment = default;
        MainUISprite.enabled = false;
        Icon.enabled = false;
        ItemName.text = "";
        ItemNameShadow.text = "";
        ItemPrice.text = "";
        Amount.text = "";
        GoldCoin.enabled = false;
    }

}
