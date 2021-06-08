using System;
using UnityEngine;

public abstract class Opener : MonoBehaviour, IOpener
{
    [SerializeField] private AbstractInventoryUIManger inventoryUIManager;

    public virtual void Awake()
    {
        inventoryUIManager = GetComponent<AbstractInventoryUIManger>();
    }

    public virtual void Interact(CharacterManager characterManager)
    {
        Debug.Log("Default interaction... " + inventoryUIManager.isOpened);
        if (!inventoryUIManager.isOpened) Open(characterManager);
        else Close(characterManager);
    }

    public virtual void Open(CharacterManager characterManager)
    {
        Debug.Log("Default open...");
        inventoryUIManager.Open();
    }

    public virtual void Close(CharacterManager characterManager)
    {
        Debug.Log("Default close...");
        inventoryUIManager.Close();
    }
}
