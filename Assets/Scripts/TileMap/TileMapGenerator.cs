using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left = 270,
    Right = 90,
    Up = 0,
    Down = 180
}

public class TileMapGenerator : MonoBehaviour
{
    public Tile tilePrefab;
    public Tile obstaclePrefab;
    public int Width = 3;
    public int Height = 3;

    public Transform tilesParent;
    public Vector2 tilesOffset;
    public List<Vector2Int> obstacleCoords = new List<Vector2Int>();
    public List<Tile> tilesList;

    // Return true if we can move in that direction
    public bool CheckIfCanMove(Vector2 startMapCoords, Direction direction)
    {
        Vector2 endMapCoords = MoveCoordsInDirection(startMapCoords, direction);
        int listIndex = MapCoordsToListIndex(endMapCoords);
        // Index out of bounds
        if (listIndex < 0 || listIndex >= tilesList.Count) return false;
        return !tilesList[listIndex].IsObstacle;
    }

    // Return the destination coords from given mapCoords and direction
    public Vector2 MoveCoordsInDirection(Vector2 startMapCoords, Direction direction)
    {
        Vector2 endMapCoords = startMapCoords;
        switch (direction)
        {
            case Direction.Left:
                endMapCoords.y -= 1;
                break;
            case Direction.Right:
                endMapCoords.y += 1;
                break;
            case Direction.Down:
                endMapCoords.x -= 1;
                break;
            case Direction.Up:
                endMapCoords.x += 1;
                break;
        }
        return endMapCoords;
    }

    public Tile GetTileFromMapCoords(Vector2 coords)
    {
        return tilesList[MapCoordsToListIndex(coords)];
    }

    public Tile GetTile(Vector2 mapCoords)
    {
        int listIndex = MapCoordsToListIndex(mapCoords);
        if (listIndex < 0 || listIndex >= tilesList.Count)
        {
            Debug.LogError("Coords is out of bounds " + mapCoords);
            return null;
        }
        return tilesList[listIndex];
    }

    public int MapCoordsToListIndex(Vector2 coords)
    {
        if (coords.x < 0 || coords.x >= Height || coords.y < 0 || coords.y >= Width)
            return -999;
        return (int)(coords.x * Width + coords.y);
    }
}
