using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileCodeBlock : ExecutableCodeBlock
{
    public PlacementSlot InputSlot;
    public PlacementSlot OutputSlot;
    [SerializeField] int _maxIterations = 30;
    protected override void Start()
    {
        base.Start();
        InputSlot.ParentBlock = this;
        OutputSlot.ParentBlock = this;
    }

    public override bool IsExecutable()
    {
        return InputSlot.PlacedBlock != null
                && OutputSlot.PlacedBlock != null && ((ExecutableCodeBlock)OutputSlot.PlacedBlock).IsExecutable();
    }

    public override int GetBlockHeight()
    {
        int height = 1;
        if (OutputSlot.PlacedBlock != null)
            height += ((ExecutableCodeBlock)OutputSlot.PlacedBlock).GetBlockHeight();
        else
            height += 1;
        return height;
    }

    protected override IEnumerator CoExecute()
    {
        ConditionalCodeBlock condition = (ConditionalCodeBlock)InputSlot.PlacedBlock;
        ExecutableCodeBlock action = (ExecutableCodeBlock)OutputSlot.PlacedBlock;
        int counter = 0;

        while (condition.CheckCondition())
        {
            action.Execute();
            while (action.IsExecuting)
            {
                yield return null;
            }
            ExecutionResult = action.ExecutionResult;

            if (!ExecutionResult)
            {
                Debug.Log("Execution " + action.gameObject.name + "failed in while");
                IsExecuting = false;
                yield break;
            }

            counter++;
            if (counter >= _maxIterations)
            {
                Debug.LogError("Max iternations reached, breaking out of loop");
                IsExecuting = false;
                yield break;
            }
        }
        yield return new WaitForSeconds(_actionCompleteTime);
        ExecutionResult = true;
        IsExecuting = false;
    }

    public override PlacementSlot[] GetPlacementSlots()
    {
        return new PlacementSlot[] { InputSlot, OutputSlot };
    }
}
