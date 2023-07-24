using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTray : MonoBehaviour
{
    List<PlacementSlot> availableSlots = new();

    /// If user grabs a code block, we display the available slots on tray 
    public void DisplaySlotsFor(CodeBlock codeBlock)
    {
        foreach (PlacementSlot slot in availableSlots)
        {
            slot.DisplaySlotHolographic(codeBlock);
        }
    }

    public void DisableHolographicSlots()
    {
        foreach (PlacementSlot slot in availableSlots)
        {
            slot.DisableSlotHolographic();
        }
    }
}
