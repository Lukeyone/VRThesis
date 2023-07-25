using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class PlacementSlot : MonoBehaviour
{
    public BlockType RequiredType;
    public CodeBlock PlacedBlock;
    MeshRenderer _renderer;
    Collider _collider;

    public void DisplaySlotHolographic(CodeBlock block)
    {
        if (PlacedBlock != null) return;
        if (block.Type != RequiredType)
        {
            Debug.Log("Not of the required type, dun care ");
            return;
        }
        _renderer.enabled = true;
        _collider.enabled = true;
    }

    public void DisableSlotHolographic()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
    }

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
        _renderer.enabled = false;
        _collider.enabled = false;
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (PlacedBlock != null) return;

        CodeBlock block = other.GetComponent<CodeBlock>();

        if (block == null)
        {
            Debug.Log("It's a " + other.gameObject.name + ", dun care");
            return;
        }
        if (block.Type != RequiredType)
        {
            return;
        }
        PlaceBlock(block);
    }

    void PlaceBlock(CodeBlock block)
    {
        if (block.Type != RequiredType)
        {
            Debug.LogError("This shouldn't happen");
            return;
        }
        PlacedBlock = block;
        block.GetComponent<XRGrabInteractable>().enabled = false;
        block.transform.parent = transform.parent;
        block.transform.localPosition = transform.localPosition;
        block.transform.localRotation = Quaternion.identity;
        block.OnPlacement();

        Debug.Log("Placed block " + block.gameObject.name + " at " + gameObject.name);
    }
}
