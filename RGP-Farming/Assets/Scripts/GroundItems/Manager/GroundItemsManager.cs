using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GroundItemsManager : Singleton<GroundItemsManager>
{
    private Player player => Player.Instance();
    
    [SerializeField] private GameObject groundItemPrefab;
    
    private List<GroundItem> groundItems = new List<GroundItem>();

    public GroundItem ForGameObject(GameObject gObject)
    {
        return groundItems.FirstOrDefault(groundItem => groundItem.gameObject.Equals(gObject));
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

                if (groundItem.currentTime > 0) groundItem.currentTime -= Time.deltaTime;

                if (!(groundItem.currentTime <= 0)) continue;
                
                switch (groundItem.state)
                {
                    case State.HIDDEN:
                        groundItem.state = State.PUBLIC;
                        groundItem.currentTime = groundItem.defaultTime;
                        groundItem.gameObject.SetActive(true);
                        break;
                    case State.PUBLIC:
                        //Don't do anything if the item is a public respawnable item
                        if (!groundItem.respawn) toRemove.Add(groundItem);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            foreach (GroundItem groundItem in toRemove) Remove(groundItem.gameObject);
        }
        catch (Exception e)
        {
            Debug.LogError("Error in ground item manager: " + e.Message);
        }
    }

    /// <summary>
    /// Handles adding a ground item
    /// </summary>
    /// <param name="groundItem"></param>
    /// <param name="position"></param>
    public void Add(Item groundItem, Vector2 position)
    {
        GameObject gObject = Instantiate(groundItemPrefab, position, Quaternion.identity);
        gObject.GetComponent<GroundItemManager>().SetSprite(groundItem.item.uiSprite);
        
        groundItems.Add(new GroundItem(groundItem, gObject));
    }
    
    /// <summary>
    /// Handles removing a ground item from a game object
    /// </summary>
    /// <param name="gameObject">The game object a player walked over</param>
    public void Remove(GameObject gameObject, bool addToInventory = false)
    {
        Remove(ForGameObject(gameObject), addToInventory);
    }

    /// <summary>
    /// Handles removing a ground item
    /// </summary>
    /// <param name="groundItem">The ground item being removed</param>
    public void Remove(GroundItem groundItem, bool addToInventory = false)
    {
        if (groundItem == null)
        {
            Debug.LogError("Ground item is null");
            return;
        }
        
        if (groundItem.state == State.PUBLIC && groundItem.respawn)
        {
            groundItem.state = State.HIDDEN;
            groundItem.currentTime = groundItem.defaultTime;
            groundItem.gameObject.SetActive(false);
        }
        else
        {
            if(addToInventory) player.CharacterInventory.AddItem(groundItem.item.item, groundItem.item.amount, true);
            groundItems.Remove(groundItem);
            Destroy(groundItem.gameObject);
        }
    }
    
}
