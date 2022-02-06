using UnityEngine;

public class CharacterUIManager : MonoBehaviour
{
    [SerializeField] private GameUIManager _currentUIOpened;

    public GameUIManager CurrentUIOpened
    {
        get => _currentUIOpened;
        set => _currentUIOpened = value;
    }
    
    
}
