using UnityEngine;

public class CursorManager : Singleton<CursorManager>
{
    [SerializeField] private Texture2D regularCursor;
    [SerializeField] private Texture2D useableInteractionCursor;
    [SerializeField] private Texture2D nonUseableInteractionCursor;
    private void Awake()
    {
        SetDefaultCursor();
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(regularCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetUsableInteractionCursor()
    {
        Cursor.SetCursor(useableInteractionCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    
    public void SetNonUsableInteractionCursor()
    {
        Cursor.SetCursor(nonUseableInteractionCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
