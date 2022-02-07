using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    [SerializeField] private int _allowedSpawns;
    [SerializeField] private float _randomSpeed;
    [SerializeField] private List<SpawnedItem> _spawnedItems = new List<SpawnedItem>(); 
    
    private void Start()
    {
        for (int index = 0; index < _allowedSpawns; index++)
        {
            GameObject gObject = GroundItemsManager.Instance().Add(new GameItem(ItemManager.Instance().ForName("Diamond")), new Vector2(-0.541f, -0.541f));
            Rigidbody2D rb = gObject.GetComponent<Rigidbody2D>();
            BoxCollider2D bC = gObject.GetComponent<BoxCollider2D>();
            rb.AddRelativeForce(Random.onUnitSphere * _randomSpeed);
            bC.enabled = false;
            _spawnedItems.Add(new SpawnedItem(rb, bC));
        }

        StartCoroutine(RemoveForce());
        //GroundItemsManager.Instance().Add(new Item(ItemManager.Instance().ForName("wood"), 10), new Vector2(-0.541f, -0.541f));
    }

    IEnumerator RemoveForce()
    {
        yield return new WaitForSeconds(1f);
        foreach (SpawnedItem spawned in _spawnedItems)
        {
            spawned.rigidbody2D.velocity = Vector2.zero;
            spawned.boxCollider2D.enabled = true;
        }

        _spawnedItems.Clear();
    }

    public class SpawnedItem
    {
        public Rigidbody2D rigidbody2D;
        public BoxCollider2D boxCollider2D;

        public SpawnedItem(Rigidbody2D pRigidbody2D, BoxCollider2D pBoxCollider2D)
        {
            rigidbody2D = pRigidbody2D;
            boxCollider2D = pBoxCollider2D;
        }
    }
}