using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : CharacterManager
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("name[enter]: " + other.name);
        CharacterHealthManager characterHealthManager = other.GetComponent<CharacterHealthManager>();
        if (characterHealthManager != null)
        {
            characterHealthManager.TakeDamage(1);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("name[stay]: " + other.name);
        CharacterHealthManager characterHealthManager = other.GetComponent<CharacterHealthManager>();
        if (characterHealthManager != null)
        {
            characterHealthManager.TakeDamage(1);
        }
    }
}
