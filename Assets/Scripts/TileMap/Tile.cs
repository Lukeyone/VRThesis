using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool IsObstacle = false;
    public Vector2 MapCoordinates = new Vector2(-1, -1);
    [SerializeField] Animator animator;
    [SerializeField] GameObject debugBridge;
    bool bridgeIsBuilt = false;
    public void SetBridgeActive(bool active)
    {
        // Don't change anything because it's the same value
        if (active == bridgeIsBuilt) return;
        bridgeIsBuilt = active;
        debugBridge.SetActive(active);
        // TODO: Remove the debug bridge and change into an animation 
        // animator.SetTrigger()
    }
}
