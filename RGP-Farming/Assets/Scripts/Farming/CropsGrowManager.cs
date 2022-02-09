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
        _cropsCycleTimer = _crops.timeBetweenGrowthStage;
    }

    private void Update()
    {
        if (_readyToHarvest) return;
        
        _cropsManager.IsWatered = _tilePlacer.CheckTileUnderObject(transform.position, TileType.WATER);
        _cropsCycleTimer -= Time.deltaTime;
        if (_cropsCycleTimer <= 0)
        {
            _cropsCycleTimer = _crops.timeBetweenGrowthStage;
            if (!PlantHasDied())
            {
                if (_currentCropCycle < _crops.spriteStages.Length)
                {
                    _cropsManager.SpriteRenderer.sprite = _crops.spriteStages[++_currentCropCycle];
                }
                if (_currentCropCycle == _crops.spriteStages.Length - 1 && !_readyToHarvest)
                {
                    _cropsManager.AmountToYield = Random.Range(_crops.harvestAmount - _crops.harvestModifier, _crops.harvestAmount + _crops.harvestModifier);
                    _readyToHarvest = true;
                }
            }
            _tilePlacer.UpdateTile(gameObject, TileType.WATER);
        }
    }

    public bool PlantHasDied()
    {
        //Set the sprite of the plant to dead.
        if (!_cropsManager.IsWatered && _crops.useOfWater)
        {
            _diseasedObject.SetActive(true);
            _readyToHarvest = true;
            return true;
        }
        return false;
    }
}
