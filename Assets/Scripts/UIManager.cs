using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UIPanel _errorPanel;
    [SerializeField] UIPanel _introPanel;
    [SerializeField] UIPanel _completePanel;
    [SerializeField] float _errorPanelDisplayDuration = 10f;

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            DisplayErrorMessage(logString);
        }
    }
    public void DisplayErrorMessage(string errMessage)
    {
        _errorPanel.SetText(errMessage);
        _errorPanel.EnablePanelFor(_errorPanelDisplayDuration);
    }

    public void DisplayIntro()
    {
        _introPanel.EnablePanelFor(float.PositiveInfinity);
    }

    public void DisplayComplete()
    {
        _completePanel.EnablePanelFor(float.PositiveInfinity);
    }
}
