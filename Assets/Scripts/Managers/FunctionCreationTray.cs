using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionCreationTray : CodeTray
{
    [SerializeField] FunctionCodeBlock _blockPrefab;
    [SerializeField] Transform _newBlockParent;
    [SerializeField] GameObject startButton;
    public UnityAction<FunctionCodeBlock> OnFunctionCodeBlockCreated;
    protected override IEnumerator CoExecute()
    {
        Debug.Log("Executing ");
        startButton.SetActive(false);
        FunctionCodeBlock block = Instantiate(_blockPrefab, _newBlockParent);
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
        yield return null;
    }
}
