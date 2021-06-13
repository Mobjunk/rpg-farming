using UnityEngine;

public class ObjectInteractionManager : InteractionManager
{
    public virtual void DestroyObject(GameObject gameObject)
    {
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
    }
}
