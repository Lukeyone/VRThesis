using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public abstract class CodeBlock : MonoBehaviour
{
    public BlockType Type;
    public abstract void Execute();
    protected XRGrabInteractable _grabInteractable;
    protected CodeTray _codeTray;
    protected virtual void Start()
    {
        _grabInteractable = GetComponent<XRGrabInteractable>();
        _grabInteractable.selectEntered.AddListener(OnGrab);
        _codeTray = FindObjectOfType<CodeTray>();
    }

    protected virtual void OnGrab(SelectEnterEventArgs args)
    {
        _codeTray.DisplaySlotsFor(this);
    }
}

public enum BlockType
{
    Executable,
    Conditional
}