using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError($"Singleton of type: {typeof(T).Name}, is missing from your scene. Before entering playmode make sure this singleton is present within your scene");
            }
            return instance;
        }
    }

    private void Awake()
    {
        var objects = FindObjectsOfType(typeof(T));

        if (objects.Length > 1)
            Debug.LogError($"Multiple Singletons of type: {typeof(T).Name}");

        instance = (T)objects[0];
    }
}