using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField] private GameUIManager currentUIOpened;

    public GameUIManager CurrentUIOpened
    {
        get => currentUIOpened;
        set => currentUIOpened = value;
    }
    
    
}
