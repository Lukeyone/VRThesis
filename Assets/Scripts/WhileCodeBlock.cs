using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileCodeBlock : ExecutableCodeBlock
{
    public PlacementSlot InputSlot;
    public PlacementSlot OutputSlot;

    public override bool IsExecutable()
    {
        return true;
    }

    public override void Execute()
    {
        throw new System.NotImplementedException();
    }

    public override void OnPlacement()
    {
        throw new System.NotImplementedException();
    }
}
