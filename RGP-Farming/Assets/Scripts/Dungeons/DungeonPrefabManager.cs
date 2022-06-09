using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DungeonPrefabManager : Singleton<DungeonPrefabManager>
{
    private TilemapVisualizer _tilemapVisualizer => TilemapVisualizer.Instance();
    
    [SerializeField] private GameObject _startRoomPrefab;
    [SerializeField] private GameObject _possibleStartPos;
    [SerializeField] private GameObject _doorPrefab;
    
    private Dictionary<Vector2Int, HashSet<Vector2Int>> _currentRooms = new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    public Dictionary<Vector2Int, HashSet<Vector2Int>> CurrentRooms => _currentRooms;

    public void Clear()
    {
        CurrentRooms.Clear();
        
        foreach(Transform child in transform)
            Destroy(child.gameObject);
    }
    
    public void DebugCurrentRooms()
    {
        int index = 0;
        foreach(KeyValuePair<Vector2Int, HashSet<Vector2Int>> entry in _currentRooms)
        {
            Debug.Log($"roomStartPos: {entry.Key}");

            if (index == 0)
            {
                Instantiate(_startRoomPrefab, new Vector3(entry.Key.x + 0.5f, entry.Key.y + 0.5f, 0), Quaternion.identity, transform);

                GetStartingPositionNearWall(entry.Value);
            }

            //foreach(Vector2Int floorPosition in entry.Value)
            //    Debug.Log($"floorPosition: " + floorPosition);

            index++;
        }
    }

    private void GetStartingPositionNearWall(HashSet<Vector2Int> pFloorPositions)
    {
        //List<Vector2Int> possibleStartPositions = new List<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> possibleStartPositions = new Dictionary<Vector2Int, Vector2Int>();
        foreach (Vector2Int position in pFloorPositions)
        {
            Vector2Int wallLocation = position + new Vector2Int(0, 1);
            Vector3Int location = _tilemapVisualizer.WallTilemap.WorldToCell((Vector3Int) wallLocation);
            TileBase tileBase = _tilemapVisualizer.WallTilemap.GetTile(location);
            if (tileBase != null)
            {
                if (possibleStartPositions.ContainsKey(position)) continue;
                    
                //possibleStartPositions.Add(position);
                possibleStartPositions.Add(position, wallLocation);
            }
        }
        
        List<Vector2Int> keyList = new List<Vector2Int>(possibleStartPositions.Keys);
        Vector2Int startPosition = keyList[Random.Range(0, keyList.Count)];

        Vector2Int wall = possibleStartPositions[startPosition];
        Instantiate(_possibleStartPos, new Vector3(startPosition.x + 0.5f, startPosition.y + 0.5f, 0), Quaternion.identity, transform);
        Instantiate(_doorPrefab, new Vector3(wall.x + 0.5f, wall.y + 0.5f, 0), Quaternion.identity, transform);


    }
}
