using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Utility;

public class DoorInteraction : InteractionManager
{
    [Header("Scene")]
    public string houseName;
    private Animator _animator => GetComponent<Animator>();

    //Linkermuisknop
    public override void OnInteraction(CharacterManager pCharacterManager)
    {
        base.OnInteraction(pCharacterManager);

        //Save the players location to be able to place him back when reloading other scene.
        Vector2 tempPlayerPosition = Player.transform.position;

        Debug.Log("Door clicked left");
       
        StartCoroutine(Animation());
    }
    
    //Rechtermuisknop
    public override void OnSecondaryInteraction(CharacterManager pCharacterManager)
    {
        base.OnSecondaryInteraction(pCharacterManager);
        Debug.Log("Door clicked right");
    }


    private IEnumerator Animation() 
    {
        Debug.Log("Coroutine played");
        _animator.enabled = true;
        yield return new WaitForSeconds(1);
        AddSceneIfNotLoaded(houseName);
        ToggleRootObjectsInScene("TestScene");
       
        
    }
}
