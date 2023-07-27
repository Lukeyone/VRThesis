using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCodeBlock : ExecutableCodeBlock
{
    public PlacementSlot InputSlot;
    public PlacementSlot OutputForTrue;
    public PlacementSlot OutputForFalse;

    public override bool IsExecutable()
    {
        return InputSlot.PlacedBlock != null && (OutputForFalse.PlacedBlock != null || OutputForTrue.PlacedBlock != null);
    }

    public override void Execute()
    {
        if (!IsExecutable())
        {
            Debug.LogError("Block is not executable");
            return;
        }

        ConditionalCodeBlock condition = (ConditionalCodeBlock)InputSlot.PlacedBlock;
        ExecutableCodeBlock execIfTrue = (ExecutableCodeBlock)OutputForTrue.PlacedBlock;
        ExecutableCodeBlock execIfFalse = (ExecutableCodeBlock)OutputForFalse.PlacedBlock;

        if (condition.CheckCondition())
        {
            execIfTrue.Execute();
        }
        else
        {
            execIfFalse.Execute();
        }
        Debug.Log("executed if code block");
    }

    public override void OnPlacement()
    {
        PlacementSlot[] slots = { InputSlot, OutputForFalse, OutputForTrue };
        _codeTray.AddPlacementSlots(slots);
    }
}
