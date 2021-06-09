using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilePlacer : MonoBehaviour
{
    public Tilemap tilesGrass;
    public Tilemap tilesDirt;
    public Tile tile;

    private Vector3Int location;
    private Vector3 mp;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (tilesDirt.GetTile(tilesDirt.WorldToCell(mp)) == null)
            {
                location = tilesGrass.WorldToCell(mp);

                tilesDirt.SetTile(location, tile);

            }
            else
            {
                Debug.Log("AlreadyDirt");
            }
            //Debug.Log(tilesGrass.GetTile(location));

        }
    }
    public void CheckDirtTile()
    {
       // tiles.GetTile(location);
    }
}
