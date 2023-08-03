using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IfCodeBlock : ExecutableCodeBlock
{
    public PlacementSlot InputSlot;
    public PlacementSlot OutputForTrue;
    public PlacementSlot OutputForFalse;

    public override bool IsExecutable()
    {
        return InputSlot.PlacedBlock != null && (OutputForFalse.PlacedBlock != null || OutputForTrue.PlacedBlock != null);
    }

    protected override IEnumerator CoExecute()
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
    }

    protected override PlacementSlot[] GetPlacementSlots()
    {
        return new PlacementSlot[] { InputSlot, OutputForFalse, OutputForTrue };
    }
}
