using System;

[Serializable]
public class CollectionLogEntry
{
    public AbstractItemData Item;
    public DateTime DateAcquired;

    public CollectionLogEntry(AbstractItemData pItem)
    {
        Item = pItem;
    }

    public void SetDateAndTime(DateTime pDateTime)
    {
        DateAcquired = pDateTime;
    }
}
