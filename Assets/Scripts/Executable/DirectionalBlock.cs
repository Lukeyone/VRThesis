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

    protected override IEnumerator CoExecute()
    {
        _mapTracker.RotateCharacter(rotatesLeft);
        ExecutionResult = true;
        yield return new WaitForSeconds(_actionCompleteTime);
        IsExecuting = false;
    }
}
