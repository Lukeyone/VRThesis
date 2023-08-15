using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileMapGenerator))]
public class TileMapGeneratorEditor : Editor
{

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        DrawDefaultInspector();

        if (GUILayout.Button("Create Map"))
        {
            GenerateTileMap();
        }
        serializedObject.ApplyModifiedProperties();
    }
    void GenerateTileMap()
    {
        TileMapGenerator script = (TileMapGenerator)target;
        Transform parent = script.tilesParent;
        Vector2 offset = script.tilesOffset;
        List<Vector2Int> obstacleCoords = script.obstacleCoords;
        // Clear existing tiles
        foreach (Tile child in parent.GetComponentsInChildren<Tile>())
        {
            DestroyImmediate(child.gameObject);
        }
        script.tilesList.Clear();

        // Generate tiles
        for (int y = 0; y < script.Height; y++)
        {
            for (int x = 0; x < script.Width; x++)
            {
                Vector3 position = new Vector3(-y * offset.y, 0, x * offset.x);
                Tile prefabToInstantiate = script.tilePrefab;
                bool isObstacle = ContainsCoordinates(obstacleCoords, new Vector2Int(y, x));
                if (isObstacle)
                {
                    prefabToInstantiate = script.obstaclePrefab;
                }
                Tile tile = Instantiate(prefabToInstantiate, parent);
                tile.gameObject.name = isObstacle ? "Obstacle " : "Passable ";
                tile.gameObject.name += y + " " + x;
                tile.MapCoordinates = new Vector2(y, x);
                tile.transform.localPosition = position;
                tile.transform.localRotation = Quaternion.identity;
                script.tilesList.Add(tile);
            }
        }
    }
    private bool ContainsCoordinates(List<Vector2Int> coordsList, Vector2Int coord)
    {
        foreach (var c in coordsList)
        {
            if (coord == c) return true;
        }
        return false;
    }
}