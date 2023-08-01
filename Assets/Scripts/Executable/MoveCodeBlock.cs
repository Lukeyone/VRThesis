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

    protected override IEnumerator CoExecute()
    {
        ExecutionResult = _mapTracker.MoveTracker();
        yield return new WaitForSeconds(_actionCompleteTime);
        IsExecuting = false;
    }
}