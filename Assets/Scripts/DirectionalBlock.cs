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
                _gameManager.RotateLeft();
                break;
            case Direction.Right:
                _gameManager.RotateRight();
                break;
        }
    }

    public override void OnPlacement()
    {
    }
}
