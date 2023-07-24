using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class PlacementSlot : MonoBehaviour
{
    public BlockType RequiredType;
    public CodeBlock PlacedBlock;
    MeshRenderer _renderer;

    public void DisplaySlotHolographic(CodeBlock block)
    {
        if (block.Type != RequiredType)
        {
            Debug.Log("Not of the required type, dun care ");
            return;
        }
        _renderer.enabled = true;
    }

    public void DisableSlotHolographic()
    {
        _renderer.enabled = false;
    }

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with slot, checking...");
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
        block.transform.parent = transform;
        block.transform.localPosition = Vector3.zero;
        block.transform.localRotation = Quaternion.identity;
        block.transform.localScale = Vector3.one;

        Debug.Log("Placed block " + block.gameObject.name + " at " + gameObject.name);
    }
}
