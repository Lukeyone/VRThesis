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
    [SerializeField] CodeTray _codeTray;
    [SerializeField] UIManager _uiManager;
    [SerializeField] UnityEvent _onExecutionStarted;
    [SerializeField] UnityEvent _onGameReseted;
    [SerializeField] UnityEvent _onGameCompleted;
    [SerializeField] UnityEvent _onGameFailed;

    private void Start()
    {
        if (!_isDebugMode)
            ResetScene();
        _codeTray.OnExecutionCompleted.AddListener(OnExecutionCompleted);
        Init();
    }

    void Init()
    {
        _tracker.Init(startTile);
        _uiManager.DisplayIntro();
    }

    public void StartExecution()
    {
        if (!_codeTray.CanStartExecution())
        {
            Debug.LogError("Can't start execution");
            return;
        }
        _onExecutionStarted?.Invoke();
        
        _codeTray.ExecuteTraySlots();
    }

    void OnExecutionCompleted(bool result)
    {
        if (result && _tracker.MapCoordinates == goalTile.MapCoordinates)
        {
            Debug.Log("Reached the end, won");
            _onGameCompleted?.Invoke();
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
