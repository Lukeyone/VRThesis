using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutableCodeBlock : CodeBlock
{
    public float ActionCompleteTime = 3;
    public bool IsExecuting { get; protected set; }
    public bool ExecutionResult { get; protected set; }

    protected override void Start()
    {
        base.Start();
        Type = BlockType.ExecutableCodeBlock;
        IsExecuting = false;
        ExecutionResult = false;
        IsExecutable();
    }

    public virtual bool IsExecutable()
    {
        return true;
    }
    public abstract void Execute();
}
