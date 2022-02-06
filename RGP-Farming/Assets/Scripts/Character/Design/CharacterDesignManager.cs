using UnityEngine;

public class CharacterDesignManager : MonoBehaviour
{

    [SerializeField] private CharacterGender _currentGender;

    [SerializeField] private CharacterSkinColor _currentSkinColor;
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