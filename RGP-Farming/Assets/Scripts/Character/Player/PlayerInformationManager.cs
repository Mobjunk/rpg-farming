using UnityEngine;

public class PlayerInformationManager : Singleton<PlayerInformationManager>
{
    [HideInInspector] public string PlayerName;
    [HideInInspector] public string FarmName;
    [HideInInspector] public string FavoriteThing;

    [HideInInspector] public int CharacterSkinColor;
    [HideInInspector] public int CharacterShirtIndex;
    [HideInInspector] public int CharacterPantsIndex;
    [HideInInspector] public int CharacterFeetIndex;
    [HideInInspector] public int CharacterHairColor;
    [HideInInspector] public int CharacterHairIndex;
    [HideInInspector] public int CharacterBeardIndex;
    [HideInInspector] public int CharacterEyesIndex;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Initialize(string pPlayerName, string pFarmName, string pFavoriteThing, int pSkinColor, int pShirtIndex, int pPantsIndex, int pFeetIndex, int pHairColor, int pHairIndex, int pBeardIndex, int pEyesIndex)
    {
        PlayerName = pPlayerName;
        FarmName = pFarmName;
        FavoriteThing = pFavoriteThing;
        CharacterSkinColor = pSkinColor;
        CharacterShirtIndex = pShirtIndex;
        CharacterPantsIndex = pPantsIndex;
        CharacterFeetIndex = pFeetIndex;
        CharacterHairColor = pHairColor;
        CharacterHairIndex = pHairIndex;
        CharacterBeardIndex = pBeardIndex;
        CharacterEyesIndex = pEyesIndex;
    }
}