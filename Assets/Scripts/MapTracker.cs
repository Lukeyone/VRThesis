using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTracker : MonoBehaviour
{
    [SerializeField] TileMapGenerator map;
    [SerializeField] Tile startTile;
    [SerializeField] Tile goalTile;
    public UnityEvent OnGoalReached;
    public UnityEvent OnLevelFailed;
    Vector2 _mapCoordinates;
    Stack<Vector2> visitedCoords = new Stack<Vector2>();

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _mapCoordinates = startTile.MapCoordinates;
        map.SetBridgeActive(_mapCoordinates, true);
        visitedCoords.Push(_mapCoordinates);
    }

    // Attempts to move the tracker and returns the result of the action
    public bool MoveTracker(Direction direction)
    {
        Vector2 destination = map.MoveCoordsInDirection(_mapCoordinates, direction);
        bool canMove = map.CheckIfCanMove(_mapCoordinates, direction) && !visitedCoords.Contains(destination); // Only allow moving past a tile once

        if (!canMove) // Fail level if cannot move
        {
            FailLevel();
            return false;
        }
        // Otherwise, move the tracker and build a new bridge
        _mapCoordinates = destination;
        map.SetBridgeActive(_mapCoordinates, true);
        visitedCoords.Push(_mapCoordinates);
        if (_mapCoordinates == goalTile.MapCoordinates)
        {
            Debug.Log("Reached goal");
            OnGoalReached?.Invoke();
        }
        return true;
    }

    public void FailLevel()
    {
        // Destroy all built bridges
        foreach (Vector2 coords in visitedCoords)
            map.SetBridgeActive(coords, false);
        visitedCoords.Clear();
        Debug.Log("Failed level");
        OnLevelFailed?.Invoke();
        Init();
    }
}
