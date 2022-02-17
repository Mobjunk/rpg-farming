using System;
using TMPro;
using UnityEngine;

public class ContractDataManager : Singleton<ContractDataManager>
{
    
    [Header("Contract Data")]
    [SerializeField] private TextMeshProUGUI _clientText;
    [SerializeField] private TextMeshProUGUI _taskText;
    [SerializeField] private TextMeshProUGUI _difficultyText;
    [SerializeField] private TextMeshProUGUI _rewardsText;
    [SerializeField] private TextMeshProUGUI _completionDateText;
    [SerializeField] private TextMeshProUGUI _expireDateText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetupContract(string pClientName, string pTask, string _pDifficulty, string pRewards, string pCompletionDate, string pExpireDate)
    {
        _clientText.text = $"Client: {pClientName}";
        _taskText.text = $"Task: {pTask}";
        _difficultyText.text = $"Difficulty: {_pDifficulty}";
        _rewardsText.text = $"Rewards: {pRewards}";
        _completionDateText.text = $"Completion date: {pCompletionDate}";
        _expireDateText.text = $"Expire date: {pExpireDate}";
        gameObject.SetActive(true);
    }
}
