using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Responsible for moving the character 
public class GameManager : MonoBehaviour
{
    [SerializeField] MapTracker _tracker;

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
