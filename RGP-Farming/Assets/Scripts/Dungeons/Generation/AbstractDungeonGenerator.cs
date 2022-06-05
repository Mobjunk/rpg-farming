using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{
    protected TilemapVisualizer _tilemapVisualizer => TilemapVisualizer.Instance();
    
    [SerializeField] protected  Vector2Int _startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        _tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
