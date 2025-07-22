using System;
using UnityEngine;
using UnityEngine.UI;

public class Cream : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;

    private bool _isPickUped;

    public bool IsPickedUp => _isPickUped;

    private void OnEnable()
    {
        _button.onClick.AddListener(PickUp);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PickUp);
    }

    private void PickUp()
    {
        if (_isPickUped)
            return;
            
        Click?.Invoke();
    }

    public void PickedUp()
    {
        _isPickUped = true;
    }

    public void Putback()
    {
        _isPickUped = false;
    }
}
