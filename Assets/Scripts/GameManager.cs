using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] MapTracker _tracker;
    [SerializeField] Vector2 _startMapCoordinates = new Vector2(-1, 1);
    [SerializeField] Vector2 _endMapCoordinates = new Vector2(-1, 1);

    void Start()
    {
        _tracker.MapCoordinates = _startMapCoordinates;
    }




    public void MoveUp()
    {
        _tracker.MoveTracker(Direction.Up);
    }

    public void MoveDown()
    {
        _tracker.MoveTracker(Direction.Down);
    }
    public void MoveLeft()
    {
        _tracker.MoveTracker(Direction.Left);
    }
    public void MoveRight()
    {
        _tracker.MoveTracker(Direction.Right);
    }

}
