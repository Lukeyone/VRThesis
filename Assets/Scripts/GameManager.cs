using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Responsible for moving the character 
public class GameManager : MonoBehaviour
{
    [SerializeField] MapTracker _tracker;

    public void RotateLeft()
    {
        _tracker.RotateCharacter(true);
    }
    public void RotateRight()
    {
        _tracker.RotateCharacter(false);
    }
    public void Move()
    {
        _tracker.MoveTracker();
    }
}
