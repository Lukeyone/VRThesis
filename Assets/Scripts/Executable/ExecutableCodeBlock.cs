using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutableCodeBlock : CodeBlock
{
    public bool ExecutionResult { get; protected set; }
    public int AttachedDepth = -1;
    /// <summary>
    ///  The height level of the code block, e.g. if it is a while block with an action, the height of the while block will be 1 + 1 = 2. Normal blocks 
    //// without an output placement slot will return 1 by default 
    /// </summary>
    public virtual int GetBlockHeight()
    {
        return 1;
    }

    public bool IsExecuting { get; set; }
    [SerializeField] protected float _actionCompleteTime = 3;

    protected override void Start()
    {
        base.Start();
        Type = BlockType.ExecutableCodeBlock;
        IsExecuting = false;
        ExecutionResult = false;
        IsExecutable();
    }


    public virtual bool IsExecutable()
    {
        return true;
    }

    public void Execute()
    {
        ExecutionResult = false;
        IsExecuting = false;
        if (!IsExecutable())
        {
            Debug.LogError("Block is not executable");
            return;
        }
        IsExecuting = true;
        StartCoroutine(CoExecute());
    }

    protected abstract IEnumerator CoExecute();
}
