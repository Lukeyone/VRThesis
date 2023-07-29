using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectionalBlock : ExecutableCodeBlock
{
    [SerializeField] bool rotatesLeft = true;
    MapTracker _mapTracker;

    protected override void Start()
    {
        base.Start();
        _mapTracker = FindObjectOfType<MapTracker>();
    }

    public override void Execute()
    {
        _mapTracker.RotateCharacter(rotatesLeft);
        ExecutionResult = true;
    }
}
