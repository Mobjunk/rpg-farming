using System;
using TMPro;
using UnityEngine;

public class ContractDataManager : Singleton<ContractDataManager>
{
    [SerializeField] private GameObject _content;

    [Header("Contract Data")]
    [SerializeField] private TextMeshProUGUI _contractInformation;

    private void Awake()
    {
        _content.SetActive(false);
    }

    public void SetupContract(string pClientName, string pTask, string _pDifficulty, string pRewards, string pCompletionDate, string pExpireDate)
    {
        string outcome = $"Client: {pClientName}\n";
        outcome += $"Task: {pTask}\n";
        //outcome += $"Difficulty: {_pDifficulty}";
        outcome += $"Rewards: {pRewards}\n";
        outcome += $"Completion date: {pCompletionDate}\n";
        outcome += $"Expire date: {pExpireDate}";
        
        _contractInformation.text = outcome;
        _content.SetActive(true);
    }
}
