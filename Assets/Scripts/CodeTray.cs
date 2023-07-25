using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTray : MonoBehaviour
{
    public List<PlacementSlot> TraySlots = new();
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

    public void ExecuteTraySlots()
    {
        foreach (PlacementSlot slot in TraySlots)
        {
            if (slot.PlacedBlock == null) continue;
            var block = (ExecutableCodeBlock)slot.PlacedBlock;
            block.CheckIfExecutable();
            if (!block.IsExecutable)
            {
                Debug.LogError("Cannot execute when slots aren't filled");
                return;
            }
        }
        foreach (PlacementSlot slot in TraySlots)
        {
            if (slot.PlacedBlock == null) continue;
            var block = (ExecutableCodeBlock)slot.PlacedBlock;
            block.Execute();
        }
    }
}
