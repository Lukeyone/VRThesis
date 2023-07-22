using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlacementSlot : MonoBehaviour
{
    public BlockType RequiredType;
    public CodeBlock PlacedBlock;
    CodeBlock placeholderBlock;
    Dictionary<Renderer, Material> _originalMatsDict = new();
    [SerializeField] Material _holographicMaterial;

    public void DisplaySlotHolographic(CodeBlock block)
    {
        if (block.Type != RequiredType)
        {
            Debug.Log("Not of the required type, dun care ");
            return;
        }

        placeholderBlock = Instantiate(block, transform);
        placeholderBlock.transform.localPosition = Vector3.zero;
        placeholderBlock.transform.localRotation = Quaternion.identity;
        placeholderBlock.transform.localScale = Vector3.one;
        Renderer[] renderers = placeholderBlock.GetComponentsInChildren<Renderer>();
        _originalMatsDict.Clear();
        foreach (Renderer r in renderers)
        {
            _originalMatsDict[r] = r.material;
            r.material = _holographicMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
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
        PlacedBlock = placeholderBlock;
        block.GetComponent<XRGrabInteractable>().enabled = false;
        Destroy(block);
        foreach (var kvp in _originalMatsDict)
        {
            kvp.Key.material = kvp.Value;
        }
        Debug.Log("Placed block " + block.gameObject.name + " at " + gameObject.name);
    }
}
