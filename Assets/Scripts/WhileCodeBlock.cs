using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileCodeBlock : ExecutableCodeBlock
{
    public PlacementSlot InputSlot;
    public PlacementSlot OutputSlot;
    [SerializeField] int _maxIterations = 30;

    public override bool IsExecutable()
    {
        return InputSlot != null && OutputSlot != null;
    }

    public override void Execute()
    {
        if (!IsExecutable())
        {
            Debug.LogError("Block is not executable");
            return;
        }
        StartCoroutine(ExecuteWhile());
    }

    IEnumerator ExecuteWhile()
    {
        ConditionalCodeBlock condition = (ConditionalCodeBlock)InputSlot.PlacedBlock;
        ExecutableCodeBlock action = (ExecutableCodeBlock)OutputSlot.PlacedBlock;
        int counter = 0;

        while (condition.CheckCondition())
        {
            action.Execute();
            yield return new WaitForSeconds(action.ActionCompleteTime);
            counter++;
            if (counter >= _maxIterations)
            {
                Debug.LogError("Max iternations reached, breaking out of loop");
                break;
            }
        }
    }

    public override void OnPlacement()
    {
        PlacementSlot[] slots = { InputSlot, OutputSlot };
        _codeTray.AddPlacementSlots(slots);
    }
}
