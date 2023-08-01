using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _fadeDuration = 1f;
    CanvasGroup _canvasGroup;
    bool _isActive = false;

    void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetPanelActive(bool active)
    {
        if (active == _isActive) return;
        _isActive = active;
        _canvasGroup.DOFade(active ? 1 : 0, _fadeDuration);
    }

    public void EnablePanelFor(float aliveDuration)
    {
        SetPanelActive(true);
        if (aliveDuration > 0)
            StartCoroutine(CoDisableAfter(aliveDuration));
    }

    IEnumerator CoDisableAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetPanelActive(false);
    }
}
