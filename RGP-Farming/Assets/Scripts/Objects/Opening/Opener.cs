using System;
using UnityEngine;

public abstract class Opener : MonoBehaviour, IOpener
{
    private AbstractInventoryUIManger inventoryUIManager;

    public virtual void Awake()
    {
        inventoryUIManager = GetComponent<AbstractInventoryUIManger>();
        inventoryUIManager.onInventoryUIClosing += OnInventoryUIClosing;
    }

    public virtual void Interact(CharacterManager characterManager)
    {
        if (!inventoryUIManager.isOpened) Open(characterManager);
        else Close(characterManager);
    }

    public virtual void Open(CharacterManager characterManager)
    {
        inventoryUIManager.Open();
    }

    public virtual void Close(CharacterManager characterManager)
    {
        inventoryUIManager.Close();
    }

    public virtual void OnInventoryUIClosing() { }
}