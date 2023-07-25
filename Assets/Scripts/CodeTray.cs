using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeTray : MonoBehaviour
{
    public List<PlacementSlot> AvailableSlots = new();

    /// If user grabs a code block, we display the available slots on tray 
    public void DisplaySlotsFor(CodeBlock codeBlock)
    {
        Debug.Log("Displaying slots for code block " + codeBlock.gameObject.name);
        foreach (PlacementSlot slot in AvailableSlots)
        {
            slot.DisplaySlotHolographic(codeBlock);
        }
    }

    public void DisableHolographicSlots()
    {
        Debug.Log("Disabling slots");

        foreach (PlacementSlot slot in AvailableSlots)
        {
            slot.DisableSlotHolographic();
        }
    }

    public void AddPlacementSlots(PlacementSlot[] slots)
    {
        AvailableSlots.AddRange(slots);
    }
}
