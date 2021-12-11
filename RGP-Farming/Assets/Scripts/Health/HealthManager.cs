using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public delegate void OnHealthChanged();
    public OnHealthChanged onHealthChanged = delegate {  };
    
    [SerializeField] private int currentHealth;

    public int CurrentHealth
    {
        get => currentHealth;
        set => currentHealth = value;
    }
    
    private int maxHealth;
    
    public int MaxHealth
    {
        get => maxHealth;
        set => maxHealth = value;
    }

    /// <summary>
    /// Handles setting the current health to the max health on awake
    /// </summary>
    public void Awake()
    {
        MaxHealth = currentHealth;
    }

    /// <summary>
    /// Handles taking damage
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDamage(int damage)
    {
        if (damage > CurrentHealth) damage = CurrentHealth;
        CurrentHealth -= damage;
        if(CurrentHealth <= 0) HandleDeath();
        onHealthChanged.Invoke();
    }

    /// <summary>
    /// Handles healing
    /// </summary>
    /// <param name="heal"></param>
    public virtual void Heal(int heal)
    {
        CurrentHealth += heal;
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        onHealthChanged.Invoke();
    }

    /// <summary>
    /// Handles the death of the game object with this script attached
    /// </summary>
    public virtual void HandleDeath() { }
}
