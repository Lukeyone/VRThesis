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
    [SerializeField] UnityEvent _onExecutionStarted;
    [SerializeField] UnityEvent _onGameReseted;
    private void Start()
    {
        if (!_isDebugMode)
            ResetScene();
        _tracker.Init(startTile);
        _codeTray.OnExecutionCompleted.AddListener(OnExecutionCompleted);
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
        if (result)
        {
            if (_tracker.MapCoordinates == goalTile.MapCoordinates)
            {
                Debug.Log("Reached the end, won");
            }
            else
            {
                Debug.Log("Didnt reach the end, failed");
            }
        }
        else
        {
            Debug.Log("Hit a wall somewhere and failed");
        }
    }

    public void ResetScene()
    {
        _codeTray.ResetTraySlots();
        _blockSpawner.ResetBlocks();
        _tracker.ResetLevel();
        _tracker.Init(startTile);
        _onGameReseted?.Invoke();
    }
}
