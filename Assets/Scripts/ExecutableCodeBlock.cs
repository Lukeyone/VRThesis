using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutableCodeBlock : CodeBlock
{
    public float ActionCompleteTime = 3;
    protected override void Start()
    {
        base.Start();
        Type = BlockType.ExecutableCodeBlock;
        IsExecutable();
    }

    public virtual bool IsExecutable()
    {
        return true;
    }
    public abstract void Execute();
}
