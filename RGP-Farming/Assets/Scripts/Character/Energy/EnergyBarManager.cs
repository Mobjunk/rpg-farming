using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarManager : MonoBehaviour
{
    private CharacterEnergyManager _characterEnergyManager;

    [SerializeField] private Image _energyBar;

    private void Start()
    {
        _characterEnergyManager = Player.Instance().GetComponent<CharacterEnergyManager>();
        _characterEnergyManager.onEnergyChanged += OnEnergyChanged;
    }

    private void OnEnergyChanged()
    {
        float oneProcent = (_characterEnergyManager.MaxEnergy / 100);
        float currentProcent = (_characterEnergyManager.CurrentEnergy / oneProcent);
        float realSize = (1 / 100F) * currentProcent;
        _energyBar.fillAmount = realSize;
    }
}
