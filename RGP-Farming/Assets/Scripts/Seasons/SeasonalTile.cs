using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.Tilemaps
{
    [Serializable]
    [CreateAssetMenu(fileName = "New Seasonal Tile", menuName = "Tiles/Seasonal Tile")]
    public class SeasonalTile : TileBase
    {
        private SeasonManager _seasonManager => SeasonManager.Instance();

        public Sprite[] SeasonalSprites;

        public Tile.ColliderType TileColliderType;

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.color = Color.white;
            tileData.transform = Matrix4x4.identity;
            if (SeasonalSprites != null)
            {
                tileData.sprite = SeasonalSprites[_seasonManager.SeasonalCount];
            }
            tileData.colliderType = TileColliderType;
        }

    }
#if UNITY_EDITOR
    [CustomEditor(typeof(SeasonalTile))]
    public class SeasonalTileEditor : Editor
    {
        private SeasonalTile tile { get { return (target as SeasonalTile); } }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Place sprites shown based on the order of Season.");
            EditorGUILayout.Space();
            Array.Resize<Sprite>(ref tile.SeasonalSprites, 4);
            for (int i = 0; i < 4; i++)
            {
                tile.SeasonalSprites[i] = (Sprite)EditorGUILayout.ObjectField("Sprite " + (i + 1), tile.SeasonalSprites[i], typeof(Sprite), false, null);
            }
            tile.TileColliderType = (Tile.ColliderType)EditorGUILayout.EnumPopup("Collider Type", tile.TileColliderType);           
            if (EditorGUI.EndChangeCheck())
                EditorUtility.SetDirty(tile);
        }
    }
#endif
}

