using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    private Player _player => Player.Instance();

    [SerializeField] private Transform _startingLocation;
    [SerializeField] private Transform _teleportLocation;
    private void OnTriggerEnter2D(Collider2D pCollision)
    {
        if (pCollision.CompareTag("Player"))
        {
            _player.transform.position = _teleportLocation.position;
        }
    }
    //Show line to location. [NOT WORKING?]
    private void OnDrawGizmos()
    {
        if (_teleportLocation != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_startingLocation.position, _teleportLocation.position);
        }
    }
}
