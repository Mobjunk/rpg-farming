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
    public bool isWatered;
    
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
        isWatered = TilePlacer.Instance().CheckTileUnderObject(gameObject, TileType.WATER);
        if (Time.time > lastUpdate + crops.timeBetweenGrowthStage)
        {
            lastUpdate = Time.time;
            TilePlacer.Instance().UpdateTile(gameObject, TileType.WATER);
            if (!PlantHasDied())
            {
                if (spriteCount < crops.spriteStages.Length)
                {
                    sr.sprite = crops.spriteStages[spriteCount++];              
                }
                if (spriteCount == crops.spriteStages.Length && !readyToHarvest)
                {
                    harvestAmount = Random.Range(crops.harvestAmount - crops.harvestModifier, crops.harvestAmount + crops.harvestModifier);
                    readyToHarvest = true;
                }
            }

        }
    }
    
    public bool PlantHasDied()
    {
        // Set the sprite of the plant to dead.
        if (!isWatered)
        {
            sr.sprite = crops.diseased;
            readyToHarvest = true;
            return true;

        }
        return false;
    }
    public void GivePlayerHarvestedItem()
    {
        if (!readyToHarvest) return;
        if (harvestAmount <= 0)
        {
            Destroy(gameObject);
            return;
        }
            
        harvestAmount--;
        Player.Instance().CharacterInventory.AddItem(crops.harvestedItem);
        if (harvestAmount <= 0)
        {
            Destroy(gameObject);
            CursorManager.Instance().SetDefaultCursor();
        }
    }
}
