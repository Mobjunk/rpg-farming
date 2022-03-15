using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterContractManager : MonoBehaviour
{
    private NotificationManager _notificationManager => NotificationManager.Instance();
    
    [SerializeField] private List<Contract> _contractsInProgress = new List<Contract>();
    public List<Contract> ContractsInProgress => _contractsInProgress;

    private void Update()
    {
        List<Contract> toRemove = new List<Contract>();
        foreach (Contract contract in _contractsInProgress)
        {
            contract.TimeRemaining -= Time.deltaTime;
            if(contract.TimeRemaining <= 0) toRemove.Add(contract);
        }

        foreach (Contract remove in toRemove)
        {
            _notificationManager.SetNotification($"You were too late with finishing the contract for: {remove.AbstractContractData.linkedNpc.name}");
            _contractsInProgress.Remove(remove);
        }

        toRemove.Clear();
    }

    public void HandleContractDevelopment(AbstractItemData pItemData)
    {
        foreach (Contract contract in _contractsInProgress.Where(contract => contract != null).Where(contract => contract.AbstractContractData.linkedItem.Equals(pItemData)))
        {
            contract.CompletedAmount++;
            Debug.Log($"{contract.CompletedAmount}/{contract.RequiredAmount} progress of the {contract.AbstractContractData.name} contract");
            break;
        }
    }

    public List<Contract> GetListForNpcData(NpcData pNpcData)
    {
        return _contractsInProgress.Where(contract => contract.AbstractContractData.linkedNpc.Equals(pNpcData) && contract.CanFinishContract()).ToList();
    }
}