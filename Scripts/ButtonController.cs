using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action ButtonClicked;

    private void Start()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnMouseDown()
    {
        ButtonClicked?.Invoke();
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke();
    }
}
