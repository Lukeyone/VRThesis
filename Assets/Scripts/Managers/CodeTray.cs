using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CodeTray : MonoBehaviour
{
    public List<PlacementSlot> TraySlots = new();
    public UnityEvent<string> OnIllegalExecution;
    public UnityEvent<bool> OnExecutionCompleted;
    List<PlacementSlot> _codeBlocksSlots = new();

    /// If user grabs a code block, we display the available slots on tray 
    public void DisplaySlotsFor(CodeBlock codeBlock)
    {
        foreach (PlacementSlot slot in TraySlots)
        {
            slot.DisplaySlotHolographic(codeBlock);
        }
        foreach (PlacementSlot slot in _codeBlocksSlots)
        {
            slot.DisplaySlotHolographic(codeBlock);
        }
    }

    /// Disable all holographic slots when the code block is released
    public void DisableHolographicSlots()
    {
        foreach (PlacementSlot slot in TraySlots)
        {
            slot.DisableSlotHolographic();
        }
        foreach (PlacementSlot slot in _codeBlocksSlots)
        {
            slot.DisableSlotHolographic();
        }
    }

    public void AddPlacementSlots(PlacementSlot[] slots)
    {
        _codeBlocksSlots.AddRange(slots);
    }

    public void ResetTraySlots()
    {
        _codeBlocksSlots.Clear();
        foreach (PlacementSlot slot in TraySlots)
        {
            slot.PlacedBlock = null;
        }
    }

    public void ExecuteTraySlots()
    {
        // Perform a check to see if there is any nonexecutable code blocks
        foreach (PlacementSlot slot in TraySlots)
        {
            if (slot.PlacedBlock == null) continue;
            var block = (ExecutableCodeBlock)slot.PlacedBlock;
            if (!block.IsExecutable())
            {
                Debug.LogError("You need to fill the slots of the " + block.gameObject.name + " block");
                OnIllegalExecution?.Invoke("You need to fill the slots of the " + block.gameObject.name + " block");
                return;
            }
        }
        // Execute em!
        StartCoroutine(ExecuteSlots());
    }

    IEnumerator ExecuteSlots()
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
            // yield return new WaitForSeconds(block.ActionCompleteTime);
        }
        OnExecutionCompleted?.Invoke(true);
    }
}
