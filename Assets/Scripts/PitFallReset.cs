using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitFallReset : MonoBehaviour
{
    public Transform ResetPoint;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CharacterController c = other.GetComponent<CharacterController>();
            c.enabled = false;
            other.transform.position = ResetPoint.transform.position;
            c.enabled = true;
        }
    }

}
