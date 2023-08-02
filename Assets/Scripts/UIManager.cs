using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] UIPanel _errorPanel;
    [SerializeField] UIPanel _introPanel;
    [SerializeField] UIPanel _completePanel;
    [SerializeField] float _panelDisplayDuration = 3f;

    public void DisplayErrorMessage(string errMessage)
    {
        _errorPanel.SetText(errMessage);
        _errorPanel.EnablePanelFor(_panelDisplayDuration);
    }

    public void DisplayIntro()
    {
        _introPanel.EnablePanelFor(_panelDisplayDuration);
    }

    public void DisplayComplete()
    {
        _completePanel.EnablePanelFor(_panelDisplayDuration);
    }
}
