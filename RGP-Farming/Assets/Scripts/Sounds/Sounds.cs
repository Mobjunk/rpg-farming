using UnityEngine;

[System.Serializable]
public abstract class Sounds
{
    public abstract void HandleSound(int pIntParameter = -1, GameObject pAttachedObject = null);

    public abstract string SoundName();
}