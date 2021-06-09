using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CropsCycle : MonoBehaviour
{
    public Crops crops;

    private float lastUpdate;
    private int spriteCount= 0;

    private SpriteRenderer sr;
    private bool readyToHarvest;
    private BoxCollider2D cropsCollider;
    private StaticObjectManager _objectManager;
    private int harvestAmount;
    private bool isWatered;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        cropsCollider = GetComponent<BoxCollider2D>();
        _objectManager = GetComponent<StaticObjectManager>();
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
                Debug.Log("Done");
                readyToHarvest = true;
                _objectManager.InteractionManager = gameObject.AddComponent<CropsInteraction>();              
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
        harvestAmount = Random.Range(crops.harvestAmount - 5, crops.harvestAmount + 5);
        Debug.Log("Harvested" + harvestAmount);
        Player.Instance().CharacterInventory.AddItem(crops.harvestedItem, harvestAmount);
    }

}
