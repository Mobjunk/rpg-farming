using UnityEngine;

[RequireComponent(typeof(CropsInteraction)), RequireComponent(typeof(CropsGrowManager)), RequireComponent(typeof(CropsTriggerManager))]
public class CropsManager : MonoBehaviour
{
    private Player _player => Player.Instance();
    
    [Header("The sprite renderer")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    
    /// <summary>
    /// The grow manager of this crop
    /// </summary>
    [Header("The grow manager")]
    [SerializeField] private CropsGrowManager _cropsGrowManager;
    public CropsGrowManager CropsGrowManager => _cropsGrowManager;
    
    /// <summary>
    /// The data linked to this crops
    /// </summary>
    [Header("The data linked to this crops")]
    [SerializeField] private Crops _crops;
    public Crops Crops => _crops;

    /// <summary>
    /// To see if the crop has been watered this stage
    /// </summary>
    private bool _isWatered;

    public bool IsWatered
    {
        get => _isWatered;
        set => _isWatered = value;
    }

    /// <summary>
    /// The amount of yield the player will receive from this crop
    /// </summary>
    private int _amountToYield;

    public int AmountToYield
    {
        get => _amountToYield;
        set => _amountToYield = value;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _cropsGrowManager = GetComponent<CropsGrowManager>();
    }

    public void HandleInteraction()
    {
        if (!_cropsGrowManager.ReadyToHarvest) return;

        Vector3Int tileLocation = CharacterPlaceObject.Instance().GetTilemaps()[1].WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _player.SetAction(new HarvestCropsManager(_player, tileLocation, gameObject, Crops.harvestedItem, _amountToYield));
        CursorManager.Instance().SetDefaultCursor();
    }
}
