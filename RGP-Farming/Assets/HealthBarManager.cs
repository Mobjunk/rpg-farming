using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private CharacterHealthManager characterHealthManager;

    [SerializeField] private RectTransform healthBar;

    private void Start()
    {
        characterHealthManager = Player.Instance().GetComponent<CharacterHealthManager>();
        characterHealthManager.onHealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged()
    {
        int oneProcent = (characterHealthManager.MaxHealth / 100);
        int currentProcent = (characterHealthManager.CurrentHealth / oneProcent);
        int realSize = Mathf.FloorToInt((54F / 100F) * currentProcent);
        healthBar.sizeDelta = new Vector2(healthBar.sizeDelta.x, realSize);
    }
}
