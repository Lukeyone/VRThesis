using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCodeBlock : ExecutableCodeBlock
{
    MapTracker _mapTracker;

    protected override void Start()
    {
        base.Start();
        _mapTracker = FindObjectOfType<MapTracker>();
    }

    public override void Execute()
    {
        Debug.Log("Movewd");
        _mapTracker.MoveTracker();
    }
}