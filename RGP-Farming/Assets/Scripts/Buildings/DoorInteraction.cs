using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Utility;

public class DoorInteraction : InteractionManager
{
    [Header("Scene")]
    public string houseName;

    private bool _doorOpen;
    private Vector2 _tempPlayerPosition;
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
        _tempPlayerPosition = Player.transform.position;

        _animator.SetBool("Open", true);

        yield return new WaitForSeconds(GetAnimationClipTime(_animator,"Door_Open"));       

        AddSceneIfNotLoaded(houseName);

        //Set players location to have him at the door
        Player.transform.position = new Vector3(0, 0, 0);

        ToggleRootObjectsInScene("TestScene");
       
        
    }
    private IEnumerator ExitDoor()
    {
        Player.transform.position = _tempPlayerPosition;

        _animator.SetBool("Open", false);
        _animator.SetBool("Close", true);
        UnloadScene(houseName);

        yield return new WaitForSeconds(GetAnimationClipTime(_animator, "Door_Close"));

    }
}
