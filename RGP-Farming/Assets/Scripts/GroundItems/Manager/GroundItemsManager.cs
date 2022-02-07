using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundItemsManager : Singleton<GroundItemsManager>
{
    private Player player => Player.Instance();
    
    [SerializeField] private GameObject _groundItemPrefab;
    
    private List<GroundItem> groundItems = new List<GroundItem>();

    public GroundItem ForGameObject(GameObject gObject)
    {
        return groundItems.FirstOrDefault(groundItem => groundItem.GameObject.Equals(gObject));
    }
    
    private void Update()
    {
        try
        {
            List<GroundItem> toRemove = new List<GroundItem>();
            //Handles looping though all ground items
            foreach (GroundItem groundItem in groundItems)
            {
                if (groundItem == null) continue;

                if (groundItem.CurrentTime > 0) groundItem.CurrentTime -= Time.deltaTime;

                if (!(groundItem.CurrentTime <= 0)) continue;
                
                switch (groundItem.State)
                {
                    case State.HIDDEN:
                        groundItem.State = State.PUBLIC;
                        groundItem.CurrentTime = groundItem.DefaultTime;
                        groundItem.GameObject.SetActive(true);
                        break;
                    case State.PUBLIC:
                        //Don't do anything if the item is a public respawnable item
                        if (!groundItem.Respawn) toRemove.Add(groundItem);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (GroundItem groundItem in toRemove) Remove(groundItem.GameObject);
        }
        catch (Exception e)
        {
            Debug.LogError("Error in ground item manager: " + e.Message);
        }
    }

    /// <summary>
    /// Handles adding a ground item
    /// </summary>
    /// <param name="pGroundItem"></param>
    /// <param name="pPosition"></param>
    public void Add(GameItem pGroundItem, Vector2 pPosition)
    {
        GameObject gObject = Instantiate(_groundItemPrefab, pPosition, Quaternion.identity);
        gObject.GetComponent<GroundItemManager>().SetSprite(pGroundItem.Item.uiSprite);
        
        groundItems.Add(new GroundItem(pGroundItem, gObject));
    }
    
    /// <summary>
    /// Handles removing a ground item from a game object
    /// </summary>
    /// <param name="pGameObject">The game object a player walked over</param>
    public void Remove(GameObject pGameObject, bool pAddToInventory = false)
    {
        Remove(ForGameObject(pGameObject), pAddToInventory);
    }

    /// <summary>
    /// Handles removing a ground item
    /// </summary>
    /// <param name="pGroundItem">The ground item being removed</param>
    public void Remove(GroundItem pGroundItem, bool pAddToInventory = false)
    {
        if (pGroundItem == null)
        {
            Debug.LogError("Ground item is null");
            return;
        }
        
        if (pGroundItem.State == State.PUBLIC && pGroundItem.Respawn)
        {
            pGroundItem.State = State.HIDDEN;
            pGroundItem.CurrentTime = pGroundItem.DefaultTime;
            pGroundItem.GameObject.SetActive(false);
        }
        else
        {
            if(pAddToInventory) player.CharacterInventory.AddItem(pGroundItem.gameItem.Item, pGroundItem.gameItem.Amount, true);
            groundItems.Remove(pGroundItem);
            Destroy(pGroundItem.GameObject);
        }
    }
    
}
