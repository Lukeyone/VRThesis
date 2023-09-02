using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IfCodeBlock : ExecutableCodeBlock
{
    public PlacementSlot InputSlot;
    public PlacementSlot OutputForTrue;
    public PlacementSlot OutputForFalse;

    protected override void Start()
    {
        base.Start();
        OutputForFalse.ParentBlock = this;
        OutputForTrue.ParentBlock = this;
        InputSlot.ParentBlock = this;
    }

    public override bool IsExecutable()
    {
        return InputSlot.PlacedBlock != null && (OutputForFalse.PlacedBlock != null && ((ExecutableCodeBlock)OutputForFalse.PlacedBlock).IsExecutable()
        || OutputForTrue.PlacedBlock != null && ((ExecutableCodeBlock)OutputForTrue.PlacedBlock).IsExecutable());
    }

    public override int GetBlockHeight()
    {
        int falseHeight = 1;
        if (OutputForFalse.PlacedBlock != null)
            falseHeight += ((ExecutableCodeBlock)OutputForFalse.PlacedBlock).GetBlockHeight();
        else
            falseHeight += 1;

        int trueHeight = 1;
        if (OutputForTrue.PlacedBlock != null)
            trueHeight += ((ExecutableCodeBlock)OutputForTrue.PlacedBlock).GetBlockHeight();
        else
            trueHeight += 1;
        return falseHeight > trueHeight ? falseHeight : trueHeight;
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

    public override PlacementSlot[] GetPlacementSlots()
    {
        return new PlacementSlot[] { InputSlot, OutputForFalse, OutputForTrue };
    }
}
