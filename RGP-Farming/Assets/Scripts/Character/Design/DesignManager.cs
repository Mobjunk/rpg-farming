public class DesignManager : Singleton<DesignManager>
{
    public Chest[] CharacterShirts;
    public Legs[] CharacterPants;
    public Feet[] CharacterFeets;
    public Hair[] CharacterHairs;
    public Beard[] CharacterBeards;
    public Eyes[] CharacterEyes;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}