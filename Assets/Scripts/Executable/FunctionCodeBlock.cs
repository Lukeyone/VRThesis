using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionCodeBlock : ExecutableCodeBlock
{
    ExecutableCodeBlock[] _blocksToExecute;
    public void Initialize(ExecutableCodeBlock[] codeBlocks)
    {
        _blocksToExecute = codeBlocks;
    }
    protected override IEnumerator CoExecute()
    {
        foreach (ExecutableCodeBlock block in _blocksToExecute)
        {
            block.Execute();
            while (block.IsExecuting)
                yield return null;
            if (!block.ExecutionResult)
            {
                Debug.Log("The block " + block.gameObject.name + " in function block returned false");
                IsExecuting = false;
                yield break;
            }
        }
        yield return new WaitForSeconds(_actionCompleteTime);
        ExecutionResult = true;
        IsExecuting = false;
    }
}
