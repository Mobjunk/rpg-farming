using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(pother == _doorcollider)
        {
            
            SceneManager.LoadScene(houseName);
        }
    }
}
