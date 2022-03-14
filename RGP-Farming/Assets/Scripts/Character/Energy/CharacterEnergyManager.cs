using UnityEngine;

public class CharacterEnergyManager : MonoBehaviour
{
    public delegate void OnEnergyChanged();
    public OnEnergyChanged onEnergyChanged = delegate {  };
    
    [SerializeField] private float _currentEnergy;

    public float CurrentEnergy
    {
        get => _currentEnergy;
        set => _currentEnergy = value;
    }
    
    private float _maxEnergy;
    
    public float MaxEnergy
    {
        get => _maxEnergy;
        set => _maxEnergy = value;
    }
    public void Awake()
    {
        MaxEnergy = _currentEnergy;
    }
    
    public void RemoveEnergy(float pEnergy)
    {
        if (pEnergy > CurrentEnergy) pEnergy = CurrentEnergy;
        CurrentEnergy -= pEnergy;
        if(CurrentEnergy <= 0) HandleExhaustion();
        onEnergyChanged.Invoke();
    }
    
    public void RestoreEnergy(float pEnergy)
    {
        CurrentEnergy += pEnergy;
        if (CurrentEnergy > MaxEnergy) CurrentEnergy = MaxEnergy;
        onEnergyChanged.Invoke();
    }

    private void HandleExhaustion()
    {
        
    }
}
