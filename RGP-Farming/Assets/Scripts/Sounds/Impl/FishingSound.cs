public class FishingSound : Sounds
{
    public override void HandleSound(int pIntParameter = -1)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/FX/Skills/Fishing/throw line"); 
    }

    public override string SoundName()
    {
        return "fishing";
    }
}
