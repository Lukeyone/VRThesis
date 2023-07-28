using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The bringer and destroyer of the code blocks
public class CodeBlockReaper : MonoBehaviour
{
    [SerializeField] List<CodeBlock> _originalBlocks;
    List<CodeBlock> _clonedBlocks = new();
    private void Start()
    {
        CloneBlocks();
    }

    void CloneBlocks()
    {
        _clonedBlocks.Clear();
        foreach (var block in _originalBlocks)
        {
            var cloned = Instantiate<CodeBlock>(block, block.transform.position, block.transform.rotation, block.transform.parent);
            cloned.gameObject.SetActive(false);
            cloned.gameObject.name = block.gameObject.name;
            _clonedBlocks.Add(cloned);
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
