using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

[RequireComponent(typeof(CropsInteraction))]
public class CropsCycle : MonoBehaviour
{
    private TilePlacer tilePlacer => TilePlacer.Instance();
    
    public Crops crops;

    private float updateTimer;
    
    [SerializeField] private int spriteCount;
    
    private SpriteRenderer sr;
    
    [SerializeField] private bool readyToHarvest;
    
    private int harvestAmount;
    
    private bool isWatered;
    
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        sr.sprite = crops.spriteStages[0];
        updateTimer = crops.timeBetweenGrowthStage;
    }

    private void Update()
    {
        if (readyToHarvest) return;
        CropsUpdater();
    }

    public void CropsUpdater()
    {
        isWatered = tilePlacer.CheckTileUnderObject(transform.position, TileType.WATER);
        updateTimer -= Time.deltaTime;
        if (updateTimer <= 0)
        {
            updateTimer = crops.timeBetweenGrowthStage;
            if (!PlantHasDied())
            {
                if (spriteCount < crops.spriteStages.Length)
                {
                    sr.sprite = crops.spriteStages[++spriteCount];
                }
                if (spriteCount == crops.spriteStages.Length - 1 && !readyToHarvest)
                {
                    harvestAmount = Random.Range(crops.harvestAmount - crops.harvestModifier, crops.harvestAmount + crops.harvestModifier);
                    readyToHarvest = true;
                }
            }
            tilePlacer.UpdateTile(gameObject, TileType.WATER);
        }
    }
    
    public bool PlantHasDied()
    {
        // Set the sprite of the plant to dead.
        if (!isWatered && crops.useOfWater)
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

    public bool HasFinishedGrowing()
    {
        return readyToHarvest;
    }
}
