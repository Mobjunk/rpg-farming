public class FishingManager : HarvestSkillManager
{
    private AbstractFishingData _abstractFishingData;
    
    public FishingManager(CharacterManager pCharacterManager, AbstractFishingData pFishingData) : base(pCharacterManager)
    {
        _abstractFishingData = pFishingData;
    }

    public override CharacterStates GetCharacterState()
    {
        throw new System.NotImplementedException();
    }

    public override float TimeRequired()
    {
        throw new System.NotImplementedException();
    }

    public override bool HasRequirements()
    {
        throw new System.NotImplementedException();
    }

    public override void ReceiveItem()
    {
        throw new System.NotImplementedException();
    }

    public override bool Successful()
    {
        throw new System.NotImplementedException();
    }

    public override AbstractItemData ItemToReceive()
    {
        return _abstractFishingData.fish;
    }
}