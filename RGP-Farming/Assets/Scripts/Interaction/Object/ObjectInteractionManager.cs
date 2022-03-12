using UnityEngine;

public class ObjectInteractionManager : InteractionManager
{
    public virtual void DestroyObject(GameObject gameObject, AbstractPlaceableItem placeableItem = null)
    {
        if (placeableItem != null)
        {
            CharacterPlaceObject placeObject = CharacterPlaceObject.Instance();
            Vector3Int tilePosition = placeObject.GetPlayerTileMap.WorldToCell(gameObject.transform.position);
            for (int width = 0; width < placeableItem.width; width++)
            {
                for (int height = 0; height < placeableItem.height; height++)
                {
                    Vector3Int currentTile = new Vector3Int(tilePosition.x + width, tilePosition.y + height, tilePosition.z);
                    placeObject.GetPlayerTileMap.SetTile(currentTile, null);
                }
            }
        }
        
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
    }
}
