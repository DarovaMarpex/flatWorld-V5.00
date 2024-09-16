using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WallTile : Tile
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/WallTile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save WallTile", "WallTile", "asset", "Save wallTile", "Assets");
        if (path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<WallTile>(), path);
    }

#endif
}
