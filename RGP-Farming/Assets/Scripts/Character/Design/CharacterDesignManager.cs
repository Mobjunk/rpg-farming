using UnityEngine;

public class CharacterDesignManager : MonoBehaviour
{

    [SerializeField] private CharacterGender currentGender;

    [SerializeField] private CharacterSkinColor currentSkinColor;
}

public enum CharacterGender
{
    MALE,
    FEMALE
}

public enum CharacterSkinColor
{
    LIGHT,
    DARK
}