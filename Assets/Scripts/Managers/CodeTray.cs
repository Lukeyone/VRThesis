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

    public bool CanStartExecution()
    {
        foreach (PlacementSlot slot in TraySlots)
        {
            if (slot.PlacedBlock == null) continue;
            var executable = (ExecutableCodeBlock)slot.PlacedBlock;
            if (!executable.IsExecutable())
            {
                OnIllegalExecution?.Invoke("You need to fill the slots of the " + executable.gameObject.name + " block");
                return false;
            }
        }
        return true;
    }

    /// If user grabs a code block, we display the available slots on tray 
    public void DisplaySlotsFor(CodeBlock codeBlock)
    {
        RemovePlacementSlotsFor(codeBlock);

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

    public void AddPlacementSlotsFor(CodeBlock codeBlock)
    {
        PlacementSlot[] slots = codeBlock.GetPlacementSlots();
        if (slots == null || slots.Length == 0) return;
        _codeBlocksSlots.AddRange(slots);
    }

    public void RemovePlacementSlotsFor(CodeBlock codeBlock)
    {
        PlacementSlot[] slots = codeBlock.GetPlacementSlots();
        if (slots == null || slots.Length == 0) return;

        foreach (var s in slots)
        {
            _codeBlocksSlots.Remove(s);
        }
    }

    public void ResetTraySlots()
    {
        _codeBlocksSlots.Clear();
        foreach (PlacementSlot slot in TraySlots)
        {
            slot.RemovePlacedBlock();
        }
    }

    public void ExecuteTraySlots()
    {
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
        }
        OnExecutionCompleted?.Invoke(true);
    }
}
