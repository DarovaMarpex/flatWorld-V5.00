using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorTile : Tile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/DoorTile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save DoorTile", "DoorTile", "asset", "Save doorTile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<DoorTile>(), path);
    }

#endif
}
