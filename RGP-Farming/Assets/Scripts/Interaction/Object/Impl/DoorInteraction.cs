using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Utility;

public class DoorInteraction : InteractionManager
{
    [Header("Scenes")]
    public string houseName;
    private bool _doorOpen;
    [HideInInspector] public Vector2 TempPlayerPosition;
    private Animator _animator => GetComponent<Animator>();

    //Linkermuisknop
    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        base.OnInteraction(pCharacterManager);
        StartCoroutine(EnterDoor());
    }
    //Rechtermuisknop
    public override void OnSecondaryInteraction(CharacterManager pCharacterManager)
    {
        base.OnSecondaryInteraction(pCharacterManager);
        Debug.Log("Door clicked right");
    }
    private IEnumerator EnterDoor() 
    {
        //Save the players location to be able to place him back when reloading other scene.
        TempPlayerPosition = Player.transform.position;
        Debug.Log(TempPlayerPosition);

        _animator.SetBool("Open", true);
        
        SoundManager.Instance().ExecuteSound("DoorOpenSound");

        yield return new WaitForSeconds(GetAnimationClipTime(_animator,"Door_Open"));       

        AddSceneIfNotLoaded(houseName);

        //Set players location to have him at the door
        Player.transform.position = new Vector3(0, 0, 0);
        ToggleRootObjectsInScene("Main Level");

        Debug.Log(TempPlayerPosition);

        
    }
    
}
