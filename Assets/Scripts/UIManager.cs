using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject _errorPanel;
    [SerializeField] TextMeshProUGUI _errorText;

    public void DisplayErrorText(string errText)
    {
        _errorText.text = errText;
        _errorPanel.SetActive(true);
    }

    
}
