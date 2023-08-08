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
    public int AttachedDepth = -1; // The depth of the block when it is attached to a slot, -1 when not attached
    /// <summary>
    ///  The height level of the code block, e.g. if it is a while block with an action, the height of the while block will be 1 + 1 = 2. Normal blocks 
    //// without an output placement slot will return 1 by default 
    /// </summary>
    public virtual int GetBlockHeight()
    {
        return 1;
    }
    [System.Serializable]
    public struct DepthScaleKVP
    {
        public int Depth;
        public float Scale;
    }
    [SerializeField] DepthScaleKVP[] ScaleAtDepths;

    protected void UpdateScale()
    {
        bool hasUpdated = false;
        foreach (var kvp in ScaleAtDepths)
        {
            if (kvp.Depth == AttachedDepth)
            {
                transform.localScale = Vector3.one * kvp.Scale;
                hasUpdated = true;
                break;
            }
        }
        if (!hasUpdated) transform.localScale = Vector3.one;
    }

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
            UpdateScale();
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
        UpdateScale();
    }
}

public enum BlockType
{
    ExecutableCodeBlock = 1,
    ConditionalCodeBlock = 2
}