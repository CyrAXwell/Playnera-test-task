using System;
using UnityEngine;
using UnityEngine.UI;

public class MakeupItemView : MonoBehaviour
{
    public event Action<MakeupItemView> Click;

    [SerializeField] private Image _makeupItemImage;
    [SerializeField] private Button _button;
    [SerializeField] private RectTransform _pickUpPoint;
    
    private MakeupItemSO _makeupItem;

    public MakeupItemSO MakeupItem => _makeupItem;

    public void Initialize(MakeupItemSO makeupItem)
    {
        _makeupItem =  makeupItem;
        _makeupItemImage.sprite = _makeupItem.Sprite;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnClick);
    }

    private void OnClick()
    {
        Click?.Invoke(this);
    }
}