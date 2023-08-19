using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string SceneName;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colldiing with " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            TriggerSceneChange();
        }
    }

    public void TriggerSceneChange()
    {
        SceneManager.LoadScene(SceneName);
    }
}
