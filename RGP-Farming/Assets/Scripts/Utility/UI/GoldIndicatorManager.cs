using TMPro;
using UnityEngine;

public class GoldIndicatorManager : Singleton<GoldIndicatorManager>
{
    [SerializeField] private int _goldAmount;
    [SerializeField] private int _placeHolderAmount;
    [SerializeField] private TextMeshProUGUI _goldText;

    public void Start()
    {
        _goldAmount = Player.Instance().CharacterInventory.GoldCoins;
        _goldText.text = $"{_goldAmount}";
    }

    public void Update()
    {
        if (_placeHolderAmount < _goldAmount)
        {
            _placeHolderAmount++;
            _goldText.text = $"{_placeHolderAmount}";
        } else if (_placeHolderAmount > _goldAmount)
        {
            _placeHolderAmount--;
            _goldText.text = $"{_placeHolderAmount}";
        }
    }

    public void UpdateCoins(int newAmount)
    {
        _placeHolderAmount = _goldAmount;
        _goldAmount = newAmount;
    }
}
