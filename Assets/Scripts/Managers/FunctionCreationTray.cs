using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionCreationTray : CodeTray
{
    [SerializeField] FunctionCodeBlock _blockPrefab;
    [SerializeField] Transform _newBlockParent;
    public UnityAction<FunctionCodeBlock> OnFunctionCodeBlockCreated;
    [SerializeField] float _creationCooldown = 5f;
    bool _isOnCooldown = false;
    List<GameObject> _spawnedFunctionBlocks = new List<GameObject>();
    public void DeleteAllSpanwedFunctionBlocks()
    {
        foreach (var b in _spawnedFunctionBlocks)
        {
            if (b != null)
            {
                Destroy(b);
            }
        }
        _spawnedFunctionBlocks.Clear();
    }

    protected override IEnumerator CoExecute()
    {
        FunctionCodeBlock block = Instantiate(_blockPrefab, _newBlockParent);
        _spawnedFunctionBlocks.Add(block.gameObject);
        List<ExecutableCodeBlock> codeBlocks = new();
        foreach (PlacementSlot slot in TraySlots)
        {
            if (slot.PlacedBlock == null) continue;
            codeBlocks.Add((ExecutableCodeBlock)slot.PlacedBlock);
            slot.PlacedBlock.DisableGFX();
        }
        block.Initialize(codeBlocks.ToArray());
        ResetTraySlots();
        OnFunctionCodeBlockCreated?.Invoke(block);
        StartCoroutine(CoStartCooldown());
        yield return null;
    }

    IEnumerator CoStartCooldown()
    {
        float timer = _creationCooldown;
        _isOnCooldown = true;
        while (timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        _isOnCooldown = false;
    }
}
