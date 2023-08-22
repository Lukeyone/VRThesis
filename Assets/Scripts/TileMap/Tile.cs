using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tile : MonoBehaviour
{
    public bool IsObstacle = false;
    public Vector2 MapCoordinates = new Vector2(-1, -1);
    [SerializeField] GameObject debugBridge;
    bool bridgeIsBuilt = false;
    public bool IsExecuting { get; private set; }
    [SerializeField] float _destroyDuration = 1f;
    [SerializeField] UnityEvent _onBridgeDestroyed;

    public void SetBridgeActive(bool active)
    {
        // Don't change anything because it's the same value
        if (active == bridgeIsBuilt) return;
        bridgeIsBuilt = active;
        debugBridge.SetActive(active);
        IsExecuting = false;
        if (!active)
        {
            IsExecuting = true;
            StartCoroutine(CoDestroyBridge());
        }
    }

    IEnumerator CoDestroyBridge()
    {
        Debug.Log("Bridge " + gameObject.name + " destroyed");
        _onBridgeDestroyed?.Invoke();
        yield return new WaitForSeconds(_destroyDuration);
        IsExecuting = false;
    }
}
