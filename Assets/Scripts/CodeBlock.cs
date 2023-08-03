using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public abstract class CodeBlock : MonoBehaviour
{
    protected XRGrabInteractable _grabInteractable;
    public BlockType Type { get; protected set; }
    protected CodeTray _codeTray;
    public PlacementSlot AttachedToSlot { get; protected set; }

    protected virtual void Start()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectEntered.AddListener(OnGrab);
        _grabInteractable.selectExited.AddListener(OnRelease);
        _codeTray = FindObjectOfType<CodeTray>();
    }

    protected void OnGrab(SelectEnterEventArgs args)
    {
        if (AttachedToSlot != null)
        {
            AttachedToSlot.RemovePlacedBlock();
            _codeTray.RemovePlacementSlots(GetPlacementSlots());
            AttachedToSlot = null;
        }
        _codeTray.DisplaySlotsFor(this);
    }

    protected virtual PlacementSlot[] GetPlacementSlots()
    {
        return new PlacementSlot[] { };
    }

    protected void OnRelease(SelectExitEventArgs args)
    {
        _codeTray.DisableHolographicSlots();
    }

    public void OnPlacement(PlacementSlot slot)
    {
        AttachedToSlot = slot;
        _codeTray.AddPlacementSlots(GetPlacementSlots());
    }
}

public enum BlockType
{
    ExecutableCodeBlock = 1,
    ConditionalCodeBlock = 2
}