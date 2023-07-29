using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanMoveCodeBlock : ConditionalCodeBlock
{
    MapTracker _mapTracker;
    protected override void Start()
    {
        base.Start();
        _mapTracker = FindObjectOfType<MapTracker>();
    }

    public override bool CheckCondition()
    {
        return _mapTracker.CheckIfCanMove();
    }
}
