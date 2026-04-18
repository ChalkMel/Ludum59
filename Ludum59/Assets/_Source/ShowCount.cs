using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ShowCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _closedCountText;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        WindowsCounter.ChangeCounter += DrawCounter;
    }

    private void DrawCounter()
    {
        _closedCountText.text = $"Your count is: {WindowsCounter.GetCount()}";
        _slider.value -= 5;
    }

    private void OnDestroy()
    {
        WindowsCounter.ChangeCounter -= DrawCounter;
    }
}
