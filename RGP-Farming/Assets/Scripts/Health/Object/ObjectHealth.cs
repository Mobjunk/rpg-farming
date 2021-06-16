public class ObjectHealth : HealthManager
{
    public override void HandleDeath()
    {
        Destroy(gameObject);
        CursorManager.Instance().SetDefaultCursor();
    }
}