using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class DirectionalBlock : CodeBlock
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
                _gameManager.MoveLeft();
                break;
            case Direction.Right:
                _gameManager.MoveRight();
                break;
            case Direction.Up:
                _gameManager.MoveUp();
                break;
            case Direction.Down:
                _gameManager.MoveDown();
                break;
        }
    }
}
