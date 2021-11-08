using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private CharacterHealthManager characterHealthManager;

    [SerializeField] private RectTransform _healthBar;

    private void Start()
    {
        //TODO: Maybe don't use instance
        characterHealthManager = Player.Instance().GetComponent<CharacterHealthManager>();
        characterHealthManager.onHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        int oneProcent = (characterHealthManager.MaxHealth / 100);
        int currentProcent = (characterHealthManager.CurrentHealth / oneProcent);
        int realSize = Mathf.FloorToInt((54F / 100F) * currentProcent);
        _healthBar.sizeDelta = new Vector2(_healthBar.sizeDelta.x, realSize);
    }
}
