using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FencePlacingManager : EditorWindow
{
    private bool _placeObjects = true;
    private GameObject _objectToInstantiate;
    private GameObject _parentObject;
    private Grid _mainGrid;
    [Header("A tilemap in the main level")]
    private Tilemap _tilemap;
    
    [MenuItem("Window/Fence Placer")]
    static void Init()
    {
        FencePlacingManager window = (FencePlacingManager) GetWindow(typeof(FencePlacingManager));
        window.Show();
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui -= CustomUpdate;
        SceneView.duringSceneGui += CustomUpdate;
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Allow fence to spawn", "(With left mouse button)");
        _placeObjects = EditorGUILayout.Toggle(_placeObjects);

        EditorGUILayout.LabelField("The grid in the main level", "");
        
        _mainGrid = (Grid)EditorGUILayout.ObjectField(_mainGrid, typeof(Grid), true);

        EditorGUILayout.LabelField("A tilema[ in the main level", "");
        _tilemap = (Tilemap)EditorGUILayout.ObjectField(_tilemap, typeof(Tilemap), true);
        
        EditorGUILayout.LabelField("The parent object where", "the fences will be spawned in");
        _parentObject = (GameObject) EditorGUILayout.ObjectField(_parentObject, typeof(GameObject), true);

        EditorGUILayout.LabelField("The prefab of the fence", "you want to spawn");
        _objectToInstantiate = (GameObject) EditorGUILayout.ObjectField(_objectToInstantiate, typeof(GameObject), true);
    }

    void CustomUpdate(SceneView pSceneView)
    {
        Event e = Event.current;

        if (e.type == EventType.MouseDown && e.button == 0 && _placeObjects)
        {
            Debug.Log("Middle Mouse was pressed");
 
            Vector3 mousePosition = Event.current.mousePosition;
            Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
            
            Vector3 tilePosition = _tilemap.WorldToCell(ray.origin);
            Vector3 position = _tilemap.GetCellCenterWorld(new Vector3Int((int)tilePosition.x, (int)tilePosition.y, (int)tilePosition.z));

            GameObject placedObject = (GameObject) PrefabUtility.InstantiatePrefab(PrefabUtility.GetCorrespondingObjectFromOriginalSource(_objectToInstantiate));
            placedObject.transform.position = new Vector3(position.x, position.y - 0.5f, position.z);
            placedObject.transform.localScale = new Vector3(1, 1, 1);
            placedObject.transform.parent = _parentObject.transform;
        }
    }
}
