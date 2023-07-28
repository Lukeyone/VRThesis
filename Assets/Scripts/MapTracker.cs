using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapTracker : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 playerPositionOffsetFromTile = new Vector3(0, .5f, 0);
    [SerializeField] TileMapGenerator map;
    [SerializeField] Tile startTile;
    [SerializeField] Tile goalTile;
    public UnityEvent OnGoalReached;
    public UnityEvent OnLevelFailed;
    Direction _facingDirection = Direction.Up;
    Vector2 _mapCoordinates;
    Stack<Vector2> visitedCoords = new Stack<Vector2>();

    private void Start()
    {
        Init();
    }

    void Init()
    {
        _facingDirection = Direction.Up;
        player.transform.rotation = Quaternion.Euler(Vector3.zero);
        MoveTrackerToCoords(startTile.MapCoordinates);
    }

    void MoveTrackerToCoords(Vector2 coords)
    {
        _mapCoordinates = coords;
        map.SetBridgeActive(_mapCoordinates, true);
        visitedCoords.Push(_mapCoordinates);
        Vector3 tilePos = map.GetTileFromMapCoords(_mapCoordinates).transform.position;
        player.transform.position = tilePos + playerPositionOffsetFromTile;
    }

    public void RotateCharacter(bool isRotateToLeft)
    {
        Debug.Log("rotated to " + (isRotateToLeft ? "Left" : "Right"));
        switch (_facingDirection)
        {
            case Direction.Left:
                _facingDirection = isRotateToLeft ? Direction.Down : Direction.Up;
                break;
            case Direction.Down:
                _facingDirection = isRotateToLeft ? Direction.Right : Direction.Left;
                break;
            case Direction.Up:
                _facingDirection = isRotateToLeft ? Direction.Left : Direction.Right;
                break;
            case Direction.Right:
                _facingDirection = isRotateToLeft ? Direction.Up : Direction.Down;
                break;
        }
        Vector3 newRot = player.transform.rotation.eulerAngles;
        newRot.y = (float)_facingDirection;
        player.transform.rotation = Quaternion.Euler(newRot);
    }

    // Attempts to move the tracker and returns the result of the action
    public bool MoveTracker()
    {
        bool canMove = CheckIfCanMove();

        if (!canMove) // Fail level if cannot move
        {
            FailLevel();
            return false;
        }
        // Otherwise, move the tracker and build a new bridge
        Vector2 destination = map.MoveCoordsInDirection(_mapCoordinates, _facingDirection);
        MoveTrackerToCoords(destination);
        if (_mapCoordinates == goalTile.MapCoordinates)
        {
            Debug.Log("Reached goal");
            OnGoalReached?.Invoke();
        }
        return true;
    }

    public bool CheckIfCanMove()
    {
        Vector2 destination = map.MoveCoordsInDirection(_mapCoordinates, _facingDirection);
        return map.CheckIfCanMove(_mapCoordinates, _facingDirection) && !visitedCoords.Contains(destination); // Only allow moving past a tile once
    }

    void FailLevel()
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
