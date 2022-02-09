using System;

[Serializable]
public class Contract
{
    public AbstractContractData AbstractContractData;
    public int RequiredAmount;
    public int CompletedAmount;
    public float TimeRemaining;
    
    public Contract(AbstractContractData pAbstractContractData, int pRequiredAmount)
    {
        AbstractContractData = pAbstractContractData;
        RequiredAmount = pRequiredAmount;
        TimeRemaining = GetTimeRequired();
    }

    public bool CanFinishContract()
    {
        return CompletedAmount >= RequiredAmount;
    }

    public void DecreaseContract(AbstractItemData pItem)
    {
        if (!CanDecreaseContract(pItem)) return;
        CompletedAmount++;
    }

    private bool CanDecreaseContract(AbstractItemData pItem)
    {
        return CompletedAmount < RequiredAmount && AbstractContractData.linkedItem.Equals(pItem);
    }

    private int GetTimeRequired()
    {
        switch (AbstractContractData.contractDifficulty)
        {
            case ContractDifficultys.EASY:
                return 20;
            case ContractDifficultys.MEDIUM:
                return 30;
            case ContractDifficultys.HARD:
                return 40;
            case ContractDifficultys.MASTER:
                return 50;
            default: return 10;
        }
    }
}
