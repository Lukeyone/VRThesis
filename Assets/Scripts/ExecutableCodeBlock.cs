using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutableCodeBlock : CodeBlock
{
    public bool IsExecutable = false;

    protected override void Start()
    {
        base.Start();
        Type = BlockType.ExecutableCodeBlock;
        CheckIfExecutable();
    }

    public abstract void CheckIfExecutable();
    public abstract void Execute();
}
