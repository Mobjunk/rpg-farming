using UnityEngine;

[RequireComponent(typeof(CharacterInventory), typeof(PlayerInvenotryUIManager), typeof(CharacterInteractionManager))]
[RequireComponent(typeof(CharacterPlaceObject), typeof(CharacterUIManager))]
public class Player : CharacterManager
{
    private ItemManager _itemManager => ItemManager.Instance();
    private static Player _intsance;

    public static Player Instance()
    {
        return _intsance;
    }

    public GameObject[] TileChecker;

    public Item ItemAboveHead;

    public SpriteRenderer ItemAboveHeadRenderer;

    /// <summary>
    /// Character interaction
    /// </summary>
    public CharacterInteractionManager CharacterInteractionManager;
    
    /// <summary>
    /// Players inventory
    /// </summary>
    public CharacterInventory CharacterInventory;

    /// <summary>
    /// The ui linked to the player's inventory
    /// </summary>
    public PlayerInvenotryUIManager PlayerInventoryUIManager;

    public CharacterPlaceObject CharacterPlaceObject;

    public CharacterUIManager CharacterUIManager;

    public CharacterControllerManager CharacterControllerManager;
    
    public override void Awake()
    {
        base.Awake();

        _intsance = this;

        CharacterInteractionManager = GetComponent<CharacterInteractionManager>();
        CharacterInventory = GetComponent<CharacterInventory>();
        PlayerInventoryUIManager = GetComponent<PlayerInvenotryUIManager>();
        CharacterPlaceObject = GetComponent<CharacterPlaceObject>();
        CharacterUIManager = GetComponent<CharacterUIManager>();
        CharacterControllerManager = GetComponent<CharacterControllerManager>();
    }

    public override void Start()
    {
        base.Start();
        if(PlayerInventoryUIManager == null) Debug.Log("PlayerInventoryUIManager null");
        if(CharacterInventory == null) Debug.Log("CharacterInventory null");
        PlayerInventoryUIManager.Initialize(CharacterInventory);
    }

    public void AddStarterItems()
    {
        CharacterInventory.AddItem(_itemManager.ForName("Pickaxe"), show: true);
        CharacterInventory.AddItem(_itemManager.ForName("Axe"), show: true);
        CharacterInventory.AddItem(_itemManager.ForName("Hoe"), show: true);
        CharacterInventory.AddItem(_itemManager.ForName("Scythe"), show: true);
        CharacterInventory.AddItem(_itemManager.ForName("Watering can"), show: true);
        /*characterInventory.AddItem(itemManager.ForName("Coal"), 100);
        characterInventory.AddItem(itemManager.ForName("Iron ore"), 100);*/
        CharacterInventory.AddItem(_itemManager.ForName("Chest"));
        CharacterInventory.AddItem(_itemManager.ForName("Furnace"));
        CharacterInventory.AddItem(_itemManager.ForName("Carrot seed"), 10, true);
        CharacterInventory.AddItem(_itemManager.ForName("Cabbage seed"), 10);
        CharacterInventory.AddItem(_itemManager.ForName("Eggplant seed"), 10);
        CharacterInventory.AddItem(_itemManager.ForName("Lemon seed"), 10);
        CharacterInventory.AddItem(_itemManager.ForName("Onion seed"), 10);
        CharacterInventory.AddItem(_itemManager.ForName("Pineapple seed"), 10);
        CharacterInventory.AddItem(_itemManager.ForName("Tomato seed"), 10);
        CharacterInventory.AddItem(_itemManager.ForName("Watermelon seed"), 10);
        /*characterInventory.AddItem(itemManager.ForName("Wood"), 50);
        characterInventory.AddItem(itemManager.ForName("Stone"), 50);*/

    }
}
