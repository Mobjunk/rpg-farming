public class TreeHealth : HealthManager
{
    public override void HandleDeath()
    {
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
    }
}