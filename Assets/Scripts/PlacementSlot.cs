using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class PlacementSlot : MonoBehaviour
{
    public BlockType RequiredType;
    public CodeBlock PlacedBlock { get; private set; }
    [SerializeField] float _blockPlaceCoolDownDuration = 2f;
    public ExecutableCodeBlock ParentBlock { get; set; } // The block that this placement slot belongs to
    bool _canPlaceBlock = true;
    MeshRenderer[] _renderers;
    Collider _collider;

    int GetSlotDepth()
    {
        int depth = 1;
        if (ParentBlock != null && ParentBlock.AttachedToSlot != null)
            depth += ParentBlock.AttachedDepth;
        return depth;
    }

    public void DisplaySlotHolographic(CodeBlock block)
    {
        if (PlacedBlock != null || block.Type != RequiredType) return;
        if (block.Type == BlockType.ExecutableCodeBlock)
        {
            var execBlock = (ExecutableCodeBlock)block;
            if (GetSlotDepth() + execBlock.GetBlockHeight() > 4)
            {
                Debug.LogError("Too damn high, it's  " + execBlock.GetBlockHeight());
                return;
            }
        }
        foreach (var r in _renderers)
        {
            r.enabled = true;
        }
        _collider.enabled = true;
    }

    public void DisableSlotHolographic()
    {
        foreach (var r in _renderers)
        {
            r.enabled = false;
        }
        _collider.enabled = false;
    }

    void Start()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
        _collider = GetComponent<Collider>();
        DisableSlotHolographic();
        _collider.isTrigger = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (PlacedBlock != null || !_canPlaceBlock) return;

        CodeBlock block = other.GetComponent<CodeBlock>();

        if (block == null)
        {
            Debug.Log("It's a " + other.gameObject.name + ", dun care");
            return;
        }
        if (block.Type != RequiredType || block.AttachedToSlot != null)
        {
            return;
        }
        PlaceBlock(block);
    }

    void PlaceBlock(CodeBlock block)
    {
        PlacedBlock = block;
        block.GetComponent<XRGrabInteractable>().enabled = false;
        _canPlaceBlock = false;
        block.AttachedDepth = GetSlotDepth();
        block.transform.parent = transform;
        block.transform.localPosition = Vector3.zero;
        block.transform.localRotation = Quaternion.identity;
        block.GetComponent<XRGrabInteractable>().enabled = true;
        block.PerformPlacement(this);
    }

    public void RemovePlacedBlock()
    {
        if (PlacedBlock == null) return;
        PlacedBlock = null;
        StartCoroutine(CoStartCooldown());
    }

    IEnumerator CoStartCooldown()
    {
        yield return new WaitForSeconds(_blockPlaceCoolDownDuration);
        _canPlaceBlock = true;
    }
}
