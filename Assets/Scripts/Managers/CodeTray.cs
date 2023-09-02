using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class CodeTray : MonoBehaviour
{
    protected List<PlacementSlot> TraySlots = new();
    public UnityAction OnExecutionStarted;
    public UnityAction<bool> OnExecutionCompleted;
    protected List<PlacementSlot> _codeBlocksSlots = new();

    protected void Awake()
    {
        TraySlots = GetComponentsInChildren<PlacementSlot>().ToList(); ;
    }

    public bool CanStartExecution()
    {
        foreach (PlacementSlot slot in TraySlots)
        {
            if (slot.PlacedBlock == null) continue;
            var executable = (ExecutableCodeBlock)slot.PlacedBlock;
            if (!executable.IsExecutable())
            {
                Debug.LogError("Missing an action or condition in the " + executable.gameObject.name);
                return false;
            }
        }

        return true;
    }

    /// If user grabs a code block, we display the available slots on tray 
    public void DisplaySlotsFor(CodeBlock codeBlock)
    {
        RemovePlacementSlotsFrom(codeBlock);
        Debug.Log("Displaying slots for " + codeBlock.gameObject.name);
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

    public void AddPlacementSlotsFrom(CodeBlock codeBlock, PlacementSlot placedSlot)
    {
        if (!TraySlots.Contains(placedSlot) && !_codeBlocksSlots.Contains(placedSlot)) return;
        PlacementSlot[] slots = codeBlock.GetPlacementSlots();
        if (slots == null || slots.Length == 0) return;
        _codeBlocksSlots.AddRange(slots);
    }

    public void RemovePlacementSlotsFrom(CodeBlock codeBlock)
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

    public void ExecuteTray()
    {
        if (!CanStartExecution())
        {
            return;
        }
        OnExecutionStarted?.Invoke();
        // Execute em!
        StartCoroutine(CoExecute());
    }

    protected abstract IEnumerator CoExecute();
}
