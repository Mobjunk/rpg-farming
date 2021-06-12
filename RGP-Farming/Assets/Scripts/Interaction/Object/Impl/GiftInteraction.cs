public class GiftInteraction : ObjectInteractionManager
{
    public override void OnInteraction(CharacterManager characterManager)
    {
        ((Player)characterManager).AddStarterItems();
        Destroy(gameObject);
    }
}
