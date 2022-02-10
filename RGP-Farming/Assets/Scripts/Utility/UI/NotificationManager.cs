using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager>
{
    [SerializeField] private GameObject _notificationPrefab;
    
    [SerializeField] private List<Notification> _currentNotifications = new List<Notification>();

    private void Update()
    {
        List<Notification> toRemove = new List<Notification>();
        foreach (Notification notification in _currentNotifications)
        {
            notification.TimeLeft -= Time.deltaTime;
            if (notification.TimeLeft <= 0) toRemove.Add(notification);
        }

        foreach (Notification remove in toRemove)
        {
            Destroy(remove.NotificationObject);
            _currentNotifications.Remove(remove);
        }
        toRemove.Clear();
    }

    public void SetNotification(string pNotificationMessage)
    {
        GameObject notification = Instantiate(_notificationPrefab, parent: transform);
        
        TextMeshProUGUI textMeshProUGUI = notification.GetComponentInChildren<TextMeshProUGUI>();
        if (textMeshProUGUI != null) textMeshProUGUI.text = pNotificationMessage;
        
        _currentNotifications.Add(new Notification(notification));
    }
}

[Serializable]
public class Notification
{
    public GameObject NotificationObject;
    public float TimeLeft;

    public Notification(GameObject pNotificationObject)
    {
        NotificationObject = pNotificationObject;
        TimeLeft = 5f;
    }
}