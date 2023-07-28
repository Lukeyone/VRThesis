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
    protected virtual void Start()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectEntered.AddListener(OnGrab);
        _grabInteractable.selectExited.AddListener(OnRelease);
        _codeTray = FindObjectOfType<CodeTray>();
    }

    protected virtual void OnGrab(SelectEnterEventArgs args)
    {
        _codeTray.DisplaySlotsFor(this);
    }

    protected virtual void OnRelease(SelectExitEventArgs args)
    {
        _codeTray.DisableHolographicSlots();
    }

    public virtual void OnPlacement()
    {
    }
}

public enum BlockType
{
    ExecutableCodeBlock = 1,
    ConditionalCodeBlock = 2
}