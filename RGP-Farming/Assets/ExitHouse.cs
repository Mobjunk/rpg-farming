using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Utility;

public class ExitHouse : DoorInteraction
{
    public string Overworld;
    public Vector2 TempPlayerLocation;
    private Player _player => Player.Instance();
    private Animator _animator => GetComponent<Animator>();
    private IEnumerator ExitDoor()
    {
        _animator.SetBool("Open", false);
        _animator.SetBool("Close", true);

        UnloadScene(houseName);

        Player.transform.position = TempPlayerPosition;

        //Turn all root objects back on
        ToggleRootObjectsInScene("TestScene",true);

        yield return new WaitForSeconds(GetAnimationClipTime(_animator, "Door_Close"));
    }
    void ExitDoorTest()
    {
        ToggleRootObjectsInScene("TestScene", true);
        UnloadScene(houseName);
        Player.transform.position = TempPlayerLocation;
    }
    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        if (pCollision.CompareTag("Player"))
        {
            ExitDoorTest();
        }
    }
}
