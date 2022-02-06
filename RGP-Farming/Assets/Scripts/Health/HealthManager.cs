using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public delegate void OnHealthChanged();
    public OnHealthChanged onHealthChanged = delegate {  };
    
    [SerializeField] private int _currentHealth;

    public int CurrentHealth
    {
        get => _currentHealth;
        set => _currentHealth = value;
    }
    
    private int _maxHealth;
    
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    /// <summary>
    /// Handles setting the current health to the max health on awake
    /// </summary>
    public void Awake()
    {
        MaxHealth = _currentHealth;
    }

    /// <summary>
    /// Handles taking damage
    /// </summary>
    /// <param name="pDamage"></param>
    public virtual void TakeDamage(int pDamage)
    {
        if (pDamage > CurrentHealth) pDamage = CurrentHealth;
        CurrentHealth -= pDamage;
        if(CurrentHealth <= 0) HandleDeath();
        onHealthChanged.Invoke();
    }

    /// <summary>
    /// Handles healing
    /// </summary>
    /// <param name="pHeal"></param>
    public virtual void Heal(int pHeal)
    {
        CurrentHealth += pHeal;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        onHealthChanged.Invoke();
    }

    /// <summary>
    /// Handles the death of the game object with this script attached
    /// </summary>
    public virtual void HandleDeath() { }
}
