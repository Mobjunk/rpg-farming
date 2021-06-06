using TMPro;
using UnityEngine;

public class GoldIndicatorManager : Singleton<GoldIndicatorManager>
{
    [SerializeField] private int goldAmount;
    [SerializeField] private int placeHolderAmount;
    [SerializeField] private TextMeshProUGUI goldText;

    public void Start()
    {
        goldAmount = Player.Instance().CharacterInventory.goldCoins;
        goldText.text = $"{goldAmount}";
    }

    public void Update()
    {
        if (placeHolderAmount < goldAmount)
        {
            placeHolderAmount++;
            goldText.text = $"{placeHolderAmount}";
        } else if (placeHolderAmount > goldAmount)
        {
            placeHolderAmount--;
            goldText.text = $"{placeHolderAmount}";
        }
    }

    public void UpdateCoins(int newAmount)
    {
        placeHolderAmount = goldAmount;
        goldAmount = newAmount;
    }
}
