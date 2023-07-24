using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConditionalCodeBlock : CodeBlock
{
    public abstract bool CheckCondition();
    protected override void Start()
    {
        base.Start();
        Type = BlockType.ConditionalCodeBlock;
    }
}
