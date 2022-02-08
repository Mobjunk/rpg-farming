
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CropsInteraction))]
public class CropsCycle : MonoBehaviour
{
    private Animator _animator;
    
    private TilePlacer _tilePlacer => TilePlacer.Instance();
    
    public Crops Crops;

    private float _updateTimer;
    
    private int _spriteCount;
    
    private SpriteRenderer _spriteRenderer;
    
    private bool _readyToHarvest;
    
    private int _harvestAmount;
    
    private bool _isWatered;

    private GameObject _diseasedObject;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _diseasedObject = transform.GetChild(0).gameObject;
        _spriteRenderer.sprite = Crops.spriteStages[0];
        _updateTimer = Crops.timeBetweenGrowthStage;
    }

    private void Update()
    {
        if (_readyToHarvest) return;
        CropsUpdater();
    }

    public void CropsUpdater()
    {
        _isWatered = false; //_tilePlacer.CheckTileUnderObject(transform.position, TileType.WATER);
        _updateTimer -= Time.deltaTime;
        if (_updateTimer <= 0)
        {
            _updateTimer = Crops.timeBetweenGrowthStage;
            if (!PlantHasDied())
            {
                if (_spriteCount < Crops.spriteStages.Length)
                {
                    _spriteRenderer.sprite = Crops.spriteStages[++_spriteCount];
                }
                if (_spriteCount == Crops.spriteStages.Length - 1 && !_readyToHarvest)
                {
                    _harvestAmount = Random.Range(Crops.harvestAmount - Crops.harvestModifier, Crops.harvestAmount + Crops.harvestModifier);
                    _readyToHarvest = true;
                }
            }
            _tilePlacer.UpdateTile(gameObject, TileType.WATER);
        }
    }
    
    public bool PlantHasDied()
    {
        //Set the sprite of the plant to dead.
        if (!_isWatered && Crops.useOfWater)
        {
            _diseasedObject.SetActive(true);
            _readyToHarvest = true;
            return true;

        }
        return false;
    }
    public void GivePlayerHarvestedItem()
    {
        if (!_readyToHarvest) return;
        
        if (_harvestAmount <= 0)
        {
            Destroy(gameObject);
            return;
        }
            
        _harvestAmount--;
        Player.Instance().CharacterInventory.AddItem(Crops.harvestedItem,pShow:true);
        
        if (_harvestAmount <= 0)
        {
            Vector3Int tileLocation = CharacterPlaceObject.Instance().GetTilemaps()[1].WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            CharacterPlaceObject.Instance().GetTilemaps()[1].SetTile(tileLocation, null);
            Destroy(gameObject);
            CursorManager.Instance().SetDefaultCursor();
        }
    }

    public bool HasFinishedGrowing()
    {
        return _readyToHarvest;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(_spriteCount > 0) _animator.SetBool("shake", true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.Equals("Player"))
        {
            CharacterMovementMananger characterMovementMananger = other.gameObject.GetComponent<CharacterMovementMananger>();
            if (!characterMovementMananger.CurrentDirection.Equals(Vector2.zero) && !_animator.GetBool("shake"))
            {
                if(_spriteCount > 0) _animator.SetBool("shake", true);
            }
        }
    }
}
