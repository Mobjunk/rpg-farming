using System;
using UnityEngine;

public class PlayerFishing : Singleton<PlayerFishing>
{
    [SerializeField] private Player _characterManager;

    private void Awake()
    {
        _characterManager = GetComponent<Player>();
    }

    public void StartFishing(AbstractFishingData pFishingData)
    {
        if (!_characterManager.CharacterInventory.HasItem(pFishingData.baitRequired))
        {
            Debug.Log("Player does not have the required bait.");
            return;
        }

        if (!_characterManager.CharacterInventory.ItemFitsInventory())
        {
            Debug.Log("No room in the inventory.");
            return;
        }
        
        
    }
}
