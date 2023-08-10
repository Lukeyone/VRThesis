using System.Collections;
using UnityEngine;

public class ExecutionTray : CodeTray
{
    protected override IEnumerator CoExecute()
    {
        foreach (PlacementSlot slot in TraySlots)
        {
            if (slot.PlacedBlock == null) continue;
            var block = (ExecutableCodeBlock)slot.PlacedBlock;
            block.Execute();

            while (block.IsExecuting)
                yield return null;

            if (!block.ExecutionResult)
            {
                Debug.Log("The block " + block.gameObject.name + " in code tray returned false");
                OnExecutionCompleted?.Invoke(false);
                yield break;
            }
        }
        OnExecutionCompleted?.Invoke(true);
    }
}
