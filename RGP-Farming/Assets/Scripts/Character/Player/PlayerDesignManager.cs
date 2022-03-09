using UnityEngine;

public class PlayerDesignManager : MonoBehaviour
{
    private PlayerInformationManager _playerInformationManager => PlayerInformationManager.Instance();
    private DesignManager _designManager => DesignManager.Instance();
    
    [SerializeField] private CharacterChestManager _characterChestManager;
    [SerializeField] private CharacterLegsManager _characterLegsManager;
    [SerializeField] private CharacterFeetManager _characterFeetManager;
    [SerializeField] private CharacterHairManager _characterHairManager;
    [SerializeField] private CharacterBeardManager _characterBeardManager;
    [SerializeField] private CharacterEyesManager _characterEyesManager;

    private void Awake()
    {
        if (_playerInformationManager != null)
        {
            //Handles setting up the character's look after designing it
            _characterChestManager.SetBodyPart(_designManager.CharacterShirts[_playerInformationManager.CharacterShirtIndex]);
            _characterLegsManager.SetBodyPart(_designManager.CharacterPants[_playerInformationManager.CharacterPantsIndex]);
            _characterFeetManager.SetBodyPart(_designManager.CharacterFeets[_playerInformationManager.CharacterFeetIndex]);
            _characterHairManager.SetBodyPart(_designManager.CharacterHairs[_playerInformationManager.CharacterHairIndex]);
            _characterBeardManager.SetBodyPart(_designManager.CharacterBeards[_playerInformationManager.CharacterBeardIndex]);
            _characterEyesManager.SetBodyPart(_designManager.CharacterEyes[_playerInformationManager.CharacterEyesIndex]);
        } 
    }
}