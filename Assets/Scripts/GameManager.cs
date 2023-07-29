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
    private void Start()
    {
        if (!_isDebugMode)
            ResetScene();
        _tracker.Init(startTile);
        _codeTray.OnExecutionCompleted.AddListener(OnExecutionCompleted);
    }

    void OnExecutionCompleted(bool result)
    {
        if (result)
        {
            Debug.Log("Executed successfully");
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
            Debug.Log("Failed to execute");
        }
    }

    public void ResetScene()
    {
        _codeTray.ResetTraySlots();
        _blockSpawner.ResetBlocks();
    }
}
