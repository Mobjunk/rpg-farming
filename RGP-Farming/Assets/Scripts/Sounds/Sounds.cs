[System.Serializable]
public abstract class Sounds
{
    public abstract void HandleSound(int pIntParameter = -1);

    public abstract string SoundName();
}