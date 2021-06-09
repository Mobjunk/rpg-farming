using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(CropsInteraction))]
public class CropsCycle : MonoBehaviour
{
    public Crops crops;

    private float lastUpdate;
    private int spriteCount= 0;

    private SpriteRenderer sr;
    private bool readyToHarvest;
    private BoxCollider2D cropsCollider;
    //private StaticObjectManager _objectManager;
    private int harvestAmount;
    private bool isWatered;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cropsCollider = GetComponent<BoxCollider2D>();
        //_objectManager = GetComponent<StaticObjectManager>();
    }

    private void Update()
    {
        if (readyToHarvest)
        {
            return;
        }
        CropsUpdater();
    }

    public void CropsUpdater()
    {
        if (Time.time > lastUpdate + crops.timeBetweenGrowthStage)
        {
            lastUpdate = Time.time;
            if (spriteCount < crops.spriteStages.Length)
            {
                sr.sprite = crops.spriteStages[spriteCount++];
            }
            if (spriteCount == crops.spriteStages.Length && !readyToHarvest)
            {
                harvestAmount = Random.Range(crops.harvestAmount - crops.harvestModifier, crops.harvestAmount + crops.harvestModifier);
                readyToHarvest = true;
                //_objectManager.InteractionManager = gameObject.AddComponent<CropsInteraction>();              
            }
        }
    }
    
    public void DiseasePlant()
    {
        // Set the sprite of the plant to dead.
        if (!isWatered)
        {
            sr.sprite = crops.diseased;
            readyToHarvest = true;

        }
    }
    public void GivePlayerHarvestedItem()
    {
        if (!readyToHarvest) return;
        
        harvestAmount--;
        Player.Instance().CharacterInventory.AddItem(crops.harvestedItem);
        if (harvestAmount <= 0)
        {
            Destroy(gameObject);
            CursorManager.Instance().SetDefaultCursor();
        }
    }

}
