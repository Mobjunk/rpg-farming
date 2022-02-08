using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadGroundItem : MonoBehaviour
{
    private GroundItemsManager _groundItemsManager => GroundItemsManager.Instance();
    private List<SpawnedItem> _spawnedItems = new List<SpawnedItem>();

    public void SetSpread(GameObject pGameObject, float pRandomSpeed, bool pRemoveAfter = false)
    {
        Rigidbody2D rb = pGameObject.GetComponent<Rigidbody2D>();
        BoxCollider2D bC = pGameObject.GetComponent<BoxCollider2D>();
        rb.AddRelativeForce(Random.onUnitSphere * pRandomSpeed);
        bC.enabled = false;
        _spawnedItems.Add(new SpawnedItem(pGameObject, rb, bC, pRemoveAfter));
        
        StartCoroutine(RemoveForce());
    }

    IEnumerator RemoveForce()
    {
        yield return new WaitForSeconds(1f);
        foreach (SpawnedItem spawned in _spawnedItems)
        {
            spawned.rigidbody2D.velocity = Vector2.zero;
            if (spawned.removeAfterwards) _groundItemsManager.Remove(spawned.gameObject);
            else spawned.boxCollider2D.enabled = true;
        }

        _spawnedItems.Clear();
    }

    public class SpawnedItem
    {
        public GameObject gameObject;
        public Rigidbody2D rigidbody2D;
        public BoxCollider2D boxCollider2D;
        public bool removeAfterwards;

        public SpawnedItem(GameObject pGameObject, Rigidbody2D pRigidbody2D, BoxCollider2D pBoxCollider2D, bool pRemoveAfterwards)
        {
            gameObject = pGameObject;
            rigidbody2D = pRigidbody2D;
            boxCollider2D = pBoxCollider2D;
            removeAfterwards = pRemoveAfterwards;
        }
    }
}