using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class WaterInteractionManager : MonoBehaviour
{
    private Player _player => Player.Instance();
    private DialogueManager _dialogueManager => DialogueManager.Instance();
    private ItemBarManager _itemBarManager => ItemBarManager.Instance();
    private CursorManager _cursorManager => CursorManager.Instance();
    

    [SerializeField] private Grid _grid;
    [SerializeField] private Tilemap _waterTiles;

    [SerializeField] private AbstractFishingData[] _possibleCatches;

    private void Update()
    {
        Vector3Int tileLocation = _waterTiles.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        TileBase tileBase = _waterTiles.GetTile(tileLocation);
        if (tileBase == null || !tileBase.name.Equals("WaterTile")) return;

        bool canInteract = Utility.CanInteractWithTile(_grid, tileLocation, _player.TileChecker, 2);
        
        //_cursorManager.SetDefaultCursor();
        
        if (!_itemBarManager.IsWearingCorrectTool(ToolType.FISHING_ROD)) return;
        
        if (_cursorManager.IsPointerOverUIElement()) return;
        
        List<AbstractFishingData> filteredFish = FilteredFish();
        
        //if (filteredFish.Count != 0) _cursorManager.SetUsableInteractionCursor();
        //else if(canInteract) _cursorManager.SetNonUsableInteractionCursor();
            
        if (!Input.GetMouseButtonDown(0)) return;
        
        if (!canInteract) return;
        
        if (filteredFish.Count != 0) _player.PlayerFishing.StartFishing(filteredFish[Random.Range(0, filteredFish.Count)], _waterTiles.GetCellCenterWorld(tileLocation));
        else _dialogueManager.StartDialogue("You do not have any bait to fish with.");
    }

    private List<AbstractFishingData> FilteredFish()
    {
        return _possibleCatches.Where(fish => _player.CharacterInventory.HasItem(fish.baitRequired)).ToList();
    }
}
