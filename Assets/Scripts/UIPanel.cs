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

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _isActive = _canvasGroup.alpha != 0;
        _canvasGroup.blocksRaycasts = _isActive;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetPanelActive(bool active)
    {
        _isActive = active;
        _canvasGroup.DOFade(active ? 1 : 0, _fadeDuration);
        _canvasGroup.interactable = active;
        _canvasGroup.blocksRaycasts = _isActive;
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
