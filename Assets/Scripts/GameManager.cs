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
    [SerializeField] Tile goalTile;
    [SerializeField] CodeBlockReaper _blockSpawner;
    [SerializeField] ExecutionTray _codeTray;
    [SerializeField] UIManager _uiManager;
    [SerializeField] UnityEvent _onExecutionStarted;
    [SerializeField] UnityEvent _onGameReseted;
    [SerializeField] UnityEvent _onGameVictory;
    [SerializeField] UnityEvent _onGameFailed;

    private void Start()
    {
        if (!_isDebugMode)
            ResetScene();
        _codeTray.OnExecutionStarted += _onExecutionStarted.Invoke;
        _codeTray.OnExecutionCompleted += OnExecutionCompleted;
        Init();
    }

    void Init()
    {
        _tracker.Init(startTile);
        _uiManager.DisplayIntro();
    }

    void OnExecutionCompleted(bool result)
    {
        if (result && _tracker.MapCoordinates == goalTile.MapCoordinates)
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
        _blockSpawner.ResetBlocks();
        _tracker.ResetLevel(startTile);
        _onGameReseted?.Invoke();
    }
}
