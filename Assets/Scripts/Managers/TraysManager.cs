using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraysManager : MonoBehaviour
{
    [SerializeField] FunctionCreationTray _functionTray;
    [SerializeField] ExecutionTray _execTray;

    void Start()
    {
        _functionTray.OnFunctionCodeBlockCreated += RegisterBlock;
    }

    public void RegisterBlock(CodeBlock block)
    {
        block.OnGrabEvent += HandleCodeBlockGrab;
        block.OnReleaseEvent += HandleCodeBlockRelease;
        block.OnPlacementEvent += HandleCodeBlockPlacement;
    }

    void HandleCodeBlockGrab(CodeBlock block)
    {
        _execTray.DisplaySlotsFor(block);
        _functionTray.DisplaySlotsFor(block);
    }

    void HandleCodeBlockRelease(CodeBlock block)
    {
        _execTray.DisableHolographicSlots();
        _functionTray.DisableHolographicSlots();
    }

    void HandleCodeBlockPlacement(CodeBlock block, PlacementSlot placedSlot)
    {
        _execTray.AddPlacementSlotsFrom(block, placedSlot);
        _functionTray.AddPlacementSlotsFrom(block, placedSlot);
    }
}
