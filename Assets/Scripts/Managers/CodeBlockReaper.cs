using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The bringer and destroyer of the code blocks
public class CodeBlockReaper : MonoBehaviour
{
    List<CodeBlock> _originalBlocks;
    List<CodeBlock> _clonedBlocks = new();
    TraysManager _traysManager;
    void Awake()
    {
        _traysManager = FindObjectOfType<TraysManager>();
        CloneBlocks();
    }

    void CloneBlocks()
    {
        RegisterCodeBlocksEvents();
        _clonedBlocks.Clear();
        foreach (var block in _originalBlocks)
        {
            var cloned = Instantiate<CodeBlock>(block, block.transform.position, block.transform.rotation, block.transform.parent);
            cloned.gameObject.SetActive(false);
            cloned.gameObject.name = block.gameObject.name;
            _clonedBlocks.Add(cloned);
        }
    }

    void RegisterCodeBlocksEvents()
    {
        if (_originalBlocks == null || _originalBlocks.Count == 0) _originalBlocks = new(GetComponentsInChildren<CodeBlock>());
        foreach (CodeBlock block in _originalBlocks)
        {
            _traysManager.RegisterBlock(block);
        }
    }

    

    public void ResetBlocks()
    {
        foreach (var block in _originalBlocks)
        {
            Destroy(block.gameObject);
        }
        _originalBlocks.Clear();
        _originalBlocks.AddRange(_clonedBlocks);
        foreach (var block in _originalBlocks)
        {
            block.gameObject.SetActive(true);
        }
        CloneBlocks();
    }
}
