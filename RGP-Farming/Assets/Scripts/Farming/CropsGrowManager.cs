using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CropsGrowManager : MonoBehaviour
{
    private TilePlacer _tilePlacer => TilePlacer.Instance();
    
    /// <summary>
    /// The crop manager
    /// </summary>
    [SerializeField] private CropsManager _cropsManager;
    
    /// <summary>
    /// The current crop cycle of this crop
    /// </summary>
    [SerializeField] private int _currentCropCycle;
    public int CurrentCropCycle
    {
        get => _currentCropCycle;
        set => _currentCropCycle = value;
    }
    
    /// <summary>
    /// To see if the crop is ready to be harvested
    /// </summary>
    private bool _readyToHarvest;
    public bool ReadyToHarvest => _readyToHarvest;

    /// <summary>
    /// The crops cycle time for this crop
    /// </summary>
    [SerializeField] private float _cropsCycleTimer;
    public float CropsCycleTimer => _cropsCycleTimer;

    /// <summary>
    /// The crops linked to this crop
    /// </summary>
    private Crops _crops => _cropsManager.Crops;

    /// <summary>
    /// The disseased prefab
    /// </summary>
    [SerializeField] private GameObject _diseasedObject;
    
    private void Awake()
    {
        _cropsManager = GetComponent<CropsManager>();
        _cropsCycleTimer = Time.unscaledTime + _crops.growthTime;
    }

    private void Update()
    {
        if (_readyToHarvest) return;
        
        _cropsManager.IsWatered = _tilePlacer.CheckTileUnderObject(transform.position, TileType.WATER);
        if (Time.unscaledTime >= _cropsCycleTimer)
        {
            if (!PlantHasDied())
            {
                if (_currentCropCycle < _crops.growStages.Length)
                {
                    _cropsManager.SpriteRenderer.sprite = _crops.growStages[++_currentCropCycle].Sprite;
                }
                if (_currentCropCycle == _crops.growStages.Length - 1 && !_readyToHarvest)
                {
                    _cropsManager.AmountToYield = Random.Range(_crops.harvestAmount - _crops.harvestModifier, _crops.harvestAmount + _crops.harvestModifier);
                    _readyToHarvest = true;
                }
            }
            _tilePlacer.UpdateTile(gameObject, TileType.WATER);
            _cropsCycleTimer = Time.unscaledTime + _crops.growthTime;
        }
    }

    public bool PlantHasDied()
    {
        //Set the sprite of the plant to dead.
        if (!_cropsManager.IsWatered && _crops.growStages[_currentCropCycle].NeedsWaterThisStage)
        {
            _diseasedObject.SetActive(true);
            _readyToHarvest = true;
            return true;
        }
        return false;
    }
}
