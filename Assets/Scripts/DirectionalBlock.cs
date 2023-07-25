using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectionalBlock : ExecutableCodeBlock
{
    public enum Direction
    {
        Left = 0,
        Right = 1,
        Up = 2,
        Down = 3
    }
    [SerializeField] Direction _moveDirection;
    GameManager _gameManager;

    protected override void Start()
    {
        base.Start();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public override void Execute()
    {
        switch (_moveDirection)
        {
            case Direction.Left:
                Debug.Log("movin left");
                _gameManager.MoveLeft();
                break;
            case Direction.Right:
                Debug.Log("movin right");
                _gameManager.MoveRight();
                break;
            case Direction.Up:
                _gameManager.MoveUp();
                Debug.Log("movin up");

                break;
            case Direction.Down:
                _gameManager.MoveDown();
                break;
        }
    }

    public override void CheckIfExecutable()
    {
        IsExecutable = true;
    }

    public override void OnPlacement()
    {
    }
}
