using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterDesignUIManager : Singleton<CharacterDesignUIManager>
{
    private DesignManager _designManager => DesignManager.Instance();
    private PlayerInformationManager _playerInformationManager => PlayerInformationManager.Instance();
    
    [Header("Character Backgrounds")]
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Sprite[] _backgroundImages;
    private int _currentBackgroundIndex;
    
    [Header("Character Rotations")]
    public int CurrentDirection = 0;
    
    private int _currentDirectionIndex;
    private int[] _realDirectionValues = {  0, 2, 3, 1 };

    [Header("Character Skin Color")]
    public int CharacterSkinColor;
    [SerializeField, Range(0, 8)] private int _maxSkinColor = 8;

    [Header("Character Shirts")]
    public int ClothesColor;
    [SerializeField] private CharacterUIChestManager _characterUIChestManager;
    private int _currentShirtIndex;

    [Header("Character Pants")]
    [SerializeField] private CharacterUILegsManager _characterUILegsManager;
    private int _currentPantsIndex;

    /// <summary>
    /// All feet related variables
    /// </summary>
    [Header("Character Feet")]
    [SerializeField] private CharacterUIFeetManager _characterUIFeetManager;
    private int _currentFeetIndex;

    /// <summary>
    /// All hair related variables
    /// </summary>
    [Header("Character Hair")]
    public int CharacterHairColor;
    [SerializeField, Range(0, 12)] private int _maxHairColor = 0;
    [SerializeField] private CharacterUIHairManager _characterUIHairManager;
    private int _currentHairIndex;
    
    /// <summary>
    /// All beard related variables
    /// </summary>
    [Header("Character Beard")]
    [SerializeField] private CharacterUIBeardManager _characterUIBeardManager;
    private int _currentBeardIndex;
    
    /// <summary>
    /// All eyes related variables
    /// </summary>
    [Header("Character Eyes")]
    [SerializeField] private CharacterUIEyesManager _characterUIEyesManager;
    private int _currentEyesIndex;

    [Header("Inputs")]
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Text _playerName;
    [SerializeField] private Text _farmName;
    [SerializeField] private Text _favoriteThing;

    [Header("Text Colors")]
    [SerializeField] private Color[] _textColors;
    
    public event CharacterInputAction OnCharacterChange = delegate {  };
    
    private void UpdateCharacter()
    {
        OnCharacterChange();
    }

    private void Start()
    {
        _backgroundImage.sprite = _backgroundImages[0];
        UpdateRotation(0);
        
        _playerName.InputField.onValueChanged.AddListener(delegate { PlayerNameChanged(); });
        _farmName.InputField.onValueChanged.AddListener(delegate { FarmNameChanged(); });
        _favoriteThing.InputField.onValueChanged.AddListener(delegate { FavoriteThingChanged(); });
    }

    /// <summary>
    /// Handles updating the rotation of the character when clicking the arrows
    /// </summary>
    /// <param name="pRotation"></param>
    public void UpdateRotation(int pRotation)
    {
        _currentDirectionIndex = Utility.WrapIndex(pRotation, _realDirectionValues.Length, _currentDirectionIndex);
        CurrentDirection = _realDirectionValues[_currentDirectionIndex];
        UpdateCharacter();
    }

    /// <summary>
    /// Handles updating the background image when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateBackgroundImage(int pIndex)
    {
        _currentBackgroundIndex = Utility.WrapIndex(pIndex, _backgroundImages.Length, _currentBackgroundIndex);
        _backgroundImage.sprite = _backgroundImages[_currentBackgroundIndex];
    }

    /// <summary>
    /// Handles updating the shirt of the player when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateShirt(int pIndex)
    {
        _characterUIChestManager.UpdateBodyPart(pIndex, ref _currentShirtIndex, _designManager.CharacterShirts);
    }

    /// <summary>
    /// Handles updating the pants of the player when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdatePants(int pIndex)
    {
        _characterUILegsManager.UpdateBodyPart(pIndex, ref _currentPantsIndex, _designManager.CharacterPants);
    }

    /// <summary>
    /// Handles updating the feet of the player when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateFeet(int pIndex)
    {
        _characterUIFeetManager.UpdateBodyPart(pIndex, ref _currentFeetIndex, _designManager.CharacterFeets);
    }

    /// <summary>
    /// Handles updating the hair styles when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateHair(int pIndex)
    {
        _characterUIHairManager.UpdateBodyPart(pIndex, ref _currentHairIndex, _designManager.CharacterHairs);
    }

    /// <summary>
    /// Handles setting a beard or not when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateBeard(int pIndex)
    {
        _characterUIBeardManager.UpdateBodyPart(pIndex, ref _currentBeardIndex, _designManager.CharacterBeards);
    }

    /// <summary>
    /// Handles updating the player his eye color when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateEyes(int pIndex)
    {
        _characterUIEyesManager.UpdateBodyPart(pIndex, ref _currentEyesIndex, _designManager.CharacterEyes);
    }

    /// <summary>
    /// Handles updating the skin color of the player when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateSkinColor(int pIndex)
    {
        CharacterSkinColor = Utility.WrapIndex(pIndex, _maxSkinColor, CharacterSkinColor);
        UpdateCharacter();
    }
    
    /// <summary>
    /// Handles updating the hair color of the player when clicking the arrows
    /// </summary>
    /// <param name="pIndex"></param>
    public void UpdateHairColor(int pIndex)
    {
        CharacterHairColor = Utility.WrapIndex(pIndex, _maxHairColor, CharacterHairColor);
        UpdateCharacter();
    }

    public void Confirm()
    {
        _playerInformationManager.Initialize(_playerName.InputField.text, _farmName.InputField.text, _favoriteThing.InputField.text, CharacterSkinColor, _currentShirtIndex, _currentPantsIndex, _currentFeetIndex, CharacterHairColor, _currentHairIndex, _currentBeardIndex, _currentEyesIndex);
        //SceneManager.LoadScene("Core");
        
        Utility.AddSceneIfNotLoaded("New Core");
        Utility.AddSceneIfNotLoaded("Main Level");
        
        SceneManager.UnloadSceneAsync("Character Design");
    }

    private void PlayerNameChanged()
    {
        _playerName.InputText.color = _textColors[_playerName.InputField.text.Equals(string.Empty) ? 0 : 1];
        _confirmButton.interactable = AllowedToConfirm();
    }

    private void FarmNameChanged()
    {
        _farmName.InputText.color = _textColors[_farmName.InputField.text.Equals(string.Empty) ? 0 : 1];
        _confirmButton.interactable = AllowedToConfirm();
    }

    private void FavoriteThingChanged()
    {
        _favoriteThing.InputText.color = _textColors[_favoriteThing.InputField.text.Equals(string.Empty) ? 0 : 1];
        _confirmButton.interactable = AllowedToConfirm();
    }

    public bool AllowedToConfirm()
    {
        return _playerName.InputField.text != string.Empty && _farmName.InputField.text != string.Empty && _favoriteThing.InputField.text != string.Empty;
    }
}

[Serializable]
public class Text
{
    public TextMeshProUGUI InputText;
    public TMP_InputField InputField;
}