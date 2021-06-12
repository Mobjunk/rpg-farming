public class TreeHealth : HealthManager
{
    public override void HandleDeath()
    {
        CursorManager.Instance().SetDefaultCursor();
        Destroy(gameObject);
    }
}