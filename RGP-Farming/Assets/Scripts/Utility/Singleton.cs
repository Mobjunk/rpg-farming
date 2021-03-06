using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance { get; set; }

    public static T Instance()
    {
        if(_instance == null) _instance = FindObjectOfType<T>();
        return _instance;
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}