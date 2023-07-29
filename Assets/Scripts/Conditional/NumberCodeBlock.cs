using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberCodeBlock : ConditionalCodeBlock
{
    [SerializeField] int _maxIterations = 3;
    [SerializeField] int _minIterations = 1;
    [SerializeField] TextMeshProUGUI _displayedText;
    int _chosenIterations;
    int _counter = 0;

    protected override void Start()
    {
        base.Start();
        _chosenIterations = _minIterations;
        UpdateDisplayedText();
    }

    public override bool CheckCondition()
    {
        _counter++;
        return _counter <= _chosenIterations;
    }

    public void IncrementChosenIterations(int value)
    {
        _chosenIterations = Mathf.Clamp(_chosenIterations + value, _minIterations, _maxIterations);
        UpdateDisplayedText();
    }

    void UpdateDisplayedText()
    {
        _displayedText.text = "Iterations: " + _chosenIterations;
    }
}
