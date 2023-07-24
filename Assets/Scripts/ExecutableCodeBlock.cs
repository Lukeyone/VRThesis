using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutableCodeBlock : CodeBlock
{
    public bool IsExecutable = false;
    public PlacementSlot InputSlot;

    protected override void Start()
    {
        base.Start();
        Type = BlockType.ExecutableCodeBlock;
    }
    
    public abstract void CheckIfExecutable();
    public abstract void Execute();
}
