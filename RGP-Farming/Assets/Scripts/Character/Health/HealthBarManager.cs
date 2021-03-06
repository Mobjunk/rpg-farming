using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    private CharacterHealthManager _characterHealthManager;

    [SerializeField] private Image _healthBar;

    private void Start()
    {
        _characterHealthManager = Player.Instance().GetComponent<CharacterHealthManager>();
        _characterHealthManager.onHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        int oneProcent = (_characterHealthManager.MaxHealth / 100);
        int currentProcent = (_characterHealthManager.CurrentHealth / oneProcent);
        float realSize = (1 / 100F) * currentProcent;
        _healthBar.fillAmount = realSize;
        //_healthBar.sizeDelta = new Vector2(_healthBar.sizeDelta.x, realSize);
    }
}
