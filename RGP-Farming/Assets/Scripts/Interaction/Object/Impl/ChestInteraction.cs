using System.Collections;
using UnityEngine;
using static Utility;

public class ChestInteraction : ObjectInteractionManager
{
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    
    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        base.OnInteraction(pCharacterManager);

        if (_itemBarManager.IsWearingCorrectTools(new[] { ToolType.AXE, ToolType.PICKAXE }))
        {
            string animationName = string.Empty;
            AbstractToolItem tool = (AbstractToolItem) _itemBarManager.GetItemSelected();
            if (tool.tooltype is ToolType.AXE) animationName = "axe_swing";//SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "axe_swing", true, true);
            else if (tool.tooltype is ToolType.PICKAXE) animationName = "pickaxe_swing";//SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), "pickaxe_swing", true, true);

            if (animationName.Equals(string.Empty) || pCharacterManager.CharacterAction is SmashChest) return;
            
            SetAnimator(pCharacterManager.CharacterStateManager.GetAnimator(), animationName, true, true);
            
            pCharacterManager.SetAction(new SmashChest(pCharacterManager, this, gameObject, GetAnimationClipTime(pCharacterManager.CharacterStateManager.GetAnimator(), animationName)));
        }
    }

    public override void OnSecondaryInteraction(CharacterManager pCharacterManager)
    {
        base.OnSecondaryInteraction(pCharacterManager);
        
        ChestOpener chestOpener = GetComponent<ChestOpener>();

        if (chestOpener == null)
        {
            Debug.Log("Chest opener is null");
            return;
        }
        
        chestOpener.Interact(pCharacterManager);
    }
}

public class SmashChest : CharacterAction
{
    private ObjectInteractionManager objectInteractionManager;
    private GameObject gameObject;
    private float _animationTime;
    private float _requiredTime;
    
    public SmashChest(CharacterManager pCharacterManager, ObjectInteractionManager pObjectInteractionManager, GameObject pGameObject, float pRequiredTime) : base(pCharacterManager)
    {
        objectInteractionManager = pObjectInteractionManager;
        gameObject = pGameObject;
        _requiredTime = pRequiredTime;
    }

    public override void Update()
    {
        base.Update();
        _animationTime += Time.deltaTime;
        if (_animationTime > _requiredTime)
        {
            ChestInventory chestInventory = gameObject.GetComponent<ChestInventory>();
            if (chestInventory.SlotsOccupied() > 0)
            {
                DialogueManager.Instance().StartDialogue("Maybe I should take the items out of the chest.");
            }
            else
            {
                AbstractPlaceableItem placeableItem = (AbstractPlaceableItem) ItemManager.Instance().ForName("Chest");
                GroundItemsManager.Instance().Add(new GameItem(placeableItem), gameObject.transform.position);

                if (CharacterManager is Player player)
                {
                    Vector3Int tilePos = player.CharacterPlaceObject.Grid.WorldToCell(gameObject.transform.position);
                    GridManager.Instance().UpdateGrid(new Vector2(tilePos.x, tilePos.y));
                }
                
                objectInteractionManager.DestroyObject(gameObject, placeableItem);
            }
            CharacterManager.SetAction(null);
        }
    }

    public override CharacterStates GetCharacterState()
    {
        return CharacterStates.NONE;
    }
}
