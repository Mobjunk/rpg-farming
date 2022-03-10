using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AutoScaleCollider : MonoBehaviour
{
    private BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        Vector2 temp = _collider.bounds.size;
        _collider.size = new Vector2(temp.x, temp.y / 2);
        _collider.offset = new Vector2(_collider.offset.x, -_collider.size.y / 2);
    }  
}
