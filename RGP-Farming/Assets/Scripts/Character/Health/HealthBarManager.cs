using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private CharacterHealthManager _characterHealthManager;

    [SerializeField] private RectTransform _healthBar;

    private void Start()
    {
        _characterHealthManager = Player.Instance().GetComponent<CharacterHealthManager>();
        _characterHealthManager.onHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        int oneProcent = (_characterHealthManager.MaxHealth / 100);
        int currentProcent = (_characterHealthManager.CurrentHealth / oneProcent);
        int realSize = Mathf.FloorToInt((54F / 100F) * currentProcent);
        _healthBar.sizeDelta = new Vector2(_healthBar.sizeDelta.x, realSize);
    }
}
