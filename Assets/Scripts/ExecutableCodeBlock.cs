using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutableCodeBlock : CodeBlock
{
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
