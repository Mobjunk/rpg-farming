using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.WSA;

[RequireComponent(typeof(CropsInteraction))]
public class CropsCycle : MonoBehaviour
{
    private TilePlacer tilePlacer => TilePlacer.Instance();
    
    public Crops crops;

    private float updateTimer;
    
    private int spriteCount;
    
    private SpriteRenderer sr;
    
    private bool readyToHarvest;
    
    private int harvestAmount;
    
    private bool isWatered;

    [SerializeField] private GameObject diseasedObject;



    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        diseasedObject = transform.GetChild(0).gameObject;
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
            //SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            //spriteRenderer.enabled = true;
            diseasedObject.SetActive(true);
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
