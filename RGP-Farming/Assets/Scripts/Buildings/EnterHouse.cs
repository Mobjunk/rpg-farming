using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Utility;

public class EnterHouse : MonoBehaviour
{
    [Header("Scene")]
    public string houseName;
    private Collider2D _doorcollider;
    private Animator _animator;
    private void Awake()
    {
        _doorcollider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider pother)
    {
        //Play animation of the door
        Utility.AddSceneIfNotLoaded(houseName);
    }
}
