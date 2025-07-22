using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Loofah : MonoBehaviour
{
    public event Action OnClick;
    
    [SerializeField] private Button _button;

    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    } 

    private void OnButtonClick()
    {
        float duration = 0.17f;
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_rectTransform.DOScale(new Vector3(1.1f, 1.1f, 0), duration));
        sequence.Append(_rectTransform.DOScale(new Vector3(1f, 1f, 0), duration));

        OnClick?.Invoke();
    }
}