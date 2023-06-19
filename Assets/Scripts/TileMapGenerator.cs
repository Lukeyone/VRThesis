using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class TileMapGenerator : MonoBehaviour
{
    [SerializeField] List<Tile> tilesList = new List<Tile>();
    [SerializeField] int mapWidth = 3;

    public bool CheckIfCanMove(Vector2 startMapCoords, Direction direction)
    {
        Vector2 endMapCoords = MoveCoordsInDirection(startMapCoords, direction);
        int listIndex = MapCoordsToListIndex(endMapCoords);
        // Index out of bounds
        if (listIndex < 0 || listIndex >= tilesList.Count) return false;
        return !tilesList[listIndex].IsObstacle;
    }

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

    public void SetBridgeActive(Vector2 mapCoords, bool active)
    {
        int listIndex = MapCoordsToListIndex(mapCoords);
        if (listIndex < 0 || listIndex >= tilesList.Count)
        {
            Debug.LogError("Coords is out of bounds " + mapCoords);
            return;
        }
        tilesList[listIndex].SetBridgeActive(active);
    }

    void Start()
    {
        SetTilesCoords();
    }

    int MapCoordsToListIndex(Vector2 coords)
    {
        return (int)(coords.x * mapWidth + coords.y);
    }

    void SetTilesCoords()
    {
        int currentRow = -1;
        for (int i = 0; i < tilesList.Count; i++)
        {
            int remainder = i % mapWidth;
            if (remainder == 0)
                currentRow++;
            tilesList[i].MapCoordinates = new Vector2(currentRow, remainder);
        }
    }
}
