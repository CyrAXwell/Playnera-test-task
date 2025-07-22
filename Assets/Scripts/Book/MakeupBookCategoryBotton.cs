
using System;
using UnityEngine;
using UnityEngine.UI;

public class MakeupBookCategoryButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;
    [SerializeField] private Image _unselectedImage;
    [SerializeField] private Image _selectedImage;

    private void OnEnable() => _button.onClick.AddListener(OnClick);

    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    public void Select()
    {
        _unselectedImage.enabled = false;
        _selectedImage.gameObject.SetActive(true);
    }

    public void Unselect()
    {
        _unselectedImage.enabled = true;
        _selectedImage.gameObject.SetActive(false);
    }

    private void OnClick() => Click?.Invoke();
}