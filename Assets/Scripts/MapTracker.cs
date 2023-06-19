using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTracker : MonoBehaviour
{
    public Vector2 MapCoordinates = new Vector2(0, 1);
    public Direction FacingDirection = Direction.Up;
    [SerializeField] TileMapGenerator map;
    Stack<Vector2> visitedCoords = new Stack<Vector2>();

    public void MoveTracker()
    {
        bool canMove = map.CheckIfCanMove(MapCoordinates, FacingDirection);
        if (canMove)
        {
            // Move the tracker and build a new bridge
            MapCoordinates = map.MoveCoordsInDirection(MapCoordinates, FacingDirection);
            map.SetBridgeActive(MapCoordinates, true);
            visitedCoords.Push(MapCoordinates);
        }
        else
        {
            // Destroy all built bridges
            foreach (Vector2 coords in visitedCoords)
                map.SetBridgeActive(MapCoordinates, false);
            visitedCoords.Clear();
        }
    }
}
