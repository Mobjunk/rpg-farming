using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionLogManager : Singleton<CollectionLogManager>
{
    private ItemManager _itemManager => ItemManager.Instance();
    private TimeManager _timeManager => TimeManager.Instance();

    [SerializeField] private List<CollectionLogEntry> _collectionLog = new List<CollectionLogEntry>();

    [SerializeField] private GameObject _entryPrefab;

    [SerializeField] private GameObject _parentEntries;
    
    private void Start()
    {
        foreach (AbstractItemData item in _itemManager.Items)
            _collectionLog.Add(new CollectionLogEntry(item));
        
        Debug.Log("collectionLog: " + _collectionLog.Count);
    }

    public void SetCollectionLogEntry(AbstractItemData pItem)
    {
        foreach (CollectionLogEntry entry in _collectionLog)
        {
            if (entry.Item.Equals(pItem))
            {
                if (entry.DateAcquired.Equals(DateTime.MinValue))
                {
                    entry.SetDateAndTime(_timeManager.CurrentGameTime);
                    //Debug.Log($"Set date and time for {pItem} with date: {entry.DateAcquired}");
                    break;
                }
            }
        }
    }

    public void Initialize()
    {
        int slot = 0;
        foreach (CollectionLogEntry entry in _collectionLog)
        {
            GameObject containment = Instantiate(_entryPrefab, _parentEntries.transform, true);
            containment.name = $"{slot}";
            containment.transform.localScale = Vector3.one;
            
            
            UIContainerbase<CollectionLogEntry> container = containment.GetComponent<CollectionLogGrid>();
            
            container.Container = null;
            container.SetIndicator(false);
            container.AllowMoving = false;
            container.SetContainment(entry);

            container.Icon.color = new Color(container.Icon.color.r, container.Icon.color.g, container.Icon.color.b, entry.DateAcquired.Equals(DateTime.MinValue) ? 0.25f : 1);

            slot++;
        }
    }

    public void ClearChildren()
    {
        foreach (Transform childParent in _parentEntries.transform)
            Destroy(childParent.gameObject);
    }
}