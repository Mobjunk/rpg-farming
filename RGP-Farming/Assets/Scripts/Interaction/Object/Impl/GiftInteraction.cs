using System;

public class GiftInteraction : ObjectInteractionManager
{
    private TestScript _testScript;
    
    private void Awake()
    {
        _testScript = GetComponent<TestScript>();
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        ((Player)characterManager).AddStarterItems();
        _testScript.UpdateGrid(true);
        DestroyObject(gameObject);
    }
}
