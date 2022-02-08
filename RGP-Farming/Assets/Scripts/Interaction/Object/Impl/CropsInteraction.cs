using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropsInteraction : ObjectInteractionManager
{
    private CropsManager _cropsManager;
    private void Awake()
    {
        _cropsManager = GetComponent<CropsManager>();
    }

    public override void OnInteraction(CharacterManager characterManager)
    {
        _cropsManager.HandleInteraction();
    }

}
