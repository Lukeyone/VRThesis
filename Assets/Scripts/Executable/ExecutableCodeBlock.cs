using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ExecutableCodeBlock : CodeBlock
{
    public bool ExecutionResult { get; protected set; }
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
