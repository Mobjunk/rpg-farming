using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsInteraction : ObjectInteractionManager
{
    private CropsCycle _cycle;
    private void Awake()
    {
        _cycle = GetComponent<CropsCycle>();
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        _cycle.GivePlayerHarvestedItem();
        Debug.Log("Crop Interaction");
        // Add x amount to player inventory
        //Destroy(gameObject);
    }

}
