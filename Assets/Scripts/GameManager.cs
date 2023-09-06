using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// Responsible for moving the character 
public class GameManager : MonoBehaviour
{
    [SerializeField] bool _isDebugMode = false;
    [SerializeField] MapTracker _tracker;
    [SerializeField] Tile startTile;
    [SerializeField] Tile[] goalTiles;
    [SerializeField] CodeBlockReaper _blockSpawner;
    ExecutionTray _codeTray;
    FunctionCreationTray _functionCreationTray;
    [SerializeField] UIManager _uiManager;
    [SerializeField] UnityEvent _onExecutionStarted;
    [SerializeField] UnityEvent _onGameReseted;
    [SerializeField] UnityEvent _onGameVictory;
    [SerializeField] UnityEvent _onGameFailed;

    private void Start()
    {
        _codeTray = FindObjectOfType<ExecutionTray>();
        _functionCreationTray = FindObjectOfType<FunctionCreationTray>();
        _codeTray.OnExecutionStarted += _onExecutionStarted.Invoke;
        _codeTray.OnExecutionCompleted += OnExecutionCompleted;
        if (!_isDebugMode)
            ResetScene();
        Init();
    }

    void Init()
    {
        _tracker.Init(startTile);
        _uiManager.DisplayIntro();
    }

    bool ReachedGoal()
    {
        foreach (var goal in goalTiles)
        {
            if (goal.MapCoordinates == _tracker.MapCoordinates) return true;
        }
        return false;
    }

    void OnExecutionCompleted(bool result)
    {
        if (result && ReachedGoal())
        {
            Debug.Log("Reached the end, won");
            _onGameVictory?.Invoke();
        }
        else
        {
            Debug.Log("Failed miserably");
            _onGameFailed?.Invoke();
        }
        _uiManager.DisplayComplete();
    }

    public void ResetScene()
    {
        _codeTray.ResetTraySlots();
        _functionCreationTray.ResetTraySlots();
        _functionCreationTray.DeleteAllSpanwedFunctionBlocks();
        _blockSpawner.ResetBlocks();
        _tracker.ResetLevel(startTile);
        _onGameReseted?.Invoke();
    }
}
