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
            ExecutionResult = false;
            return;
        }
        IsExecuting = true;
        StartCoroutine(ExecuteIf());
    }
    IEnumerator ExecuteIf()
    {
        ConditionalCodeBlock condition = (ConditionalCodeBlock)InputSlot.PlacedBlock;
        CodeBlock executedBlock = condition.CheckCondition() ? OutputForTrue.PlacedBlock : OutputForFalse.PlacedBlock;
        ExecutableCodeBlock execBlock = (ExecutableCodeBlock)executedBlock;

        execBlock.Execute();
        while (execBlock.IsExecuting)
        {
            yield return null;
        }
        ExecutionResult = execBlock.ExecutionResult;
        IsExecuting = false;
        Debug.Log("executed if code block");
    }

    public override void OnPlacement()
    {
        PlacementSlot[] slots = { InputSlot, OutputForFalse, OutputForTrue };
        _codeTray.AddPlacementSlots(slots);
    }
}
