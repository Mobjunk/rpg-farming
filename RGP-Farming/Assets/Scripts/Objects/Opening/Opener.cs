using System;
using UnityEngine;

public abstract class Opener : MonoBehaviour, IOpener
{
    [SerializeField] private AbstractInventoryUIManger _inventoryUIManager;

    public virtual void Awake()
    {
        _inventoryUIManager = GetComponent<AbstractInventoryUIManger>();
    }

    public virtual void Interact(CharacterManager pCharacterManager)
    {
        if (!_inventoryUIManager.IsOpened) Open(pCharacterManager);
        else Close(pCharacterManager);
    }

    public virtual void Open(CharacterManager pCharacterManager)
    {
        _inventoryUIManager.Open();
        //((Player)characterManager).ToggleInput();
        _inventoryUIManager.onInventoryUIClosing += OnInventoryUIClosing;
    }

    public virtual void Close(CharacterManager pCharacterManager)
    {
        Debug.Log("ooooof");
        _inventoryUIManager.Close();
        //((Player)characterManager).ToggleInput();
        _inventoryUIManager.onInventoryUIClosing -= OnInventoryUIClosing;
    }

    public virtual void OnInventoryUIClosing()
    {
        Close(Player.Instance());
        //Player.Instance().ToggleInput();
        //inventoryUIManager.onInventoryUIClosing -= OnInventoryUIClosing;
    }
}
