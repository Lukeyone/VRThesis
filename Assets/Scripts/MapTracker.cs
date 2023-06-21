using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTracker : MonoBehaviour
{
    [SerializeField] TileMapGenerator map;
    Vector2 _mapCoordinates;
    Stack<Vector2> visitedCoords = new Stack<Vector2>();
    public Vector2 MapCoordinates
    {
        get
        {
            return _mapCoordinates;
        }
        set
        {
            _mapCoordinates = value;
        }
    }

    public bool MoveTracker(Direction direction)
    {
        bool canMove = map.CheckIfCanMove(_mapCoordinates, direction);
        if (canMove)
        {
            // Move the tracker and build a new bridge
            _mapCoordinates = map.MoveCoordsInDirection(_mapCoordinates, direction);
            map.SetBridgeActive(_mapCoordinates, true);
            visitedCoords.Push(_mapCoordinates);
            return true;
        }
        // Destroy all built bridges
        foreach (Vector2 coords in visitedCoords)
            map.SetBridgeActive(coords, false);
        visitedCoords.Clear();
        return false;
    }
}
