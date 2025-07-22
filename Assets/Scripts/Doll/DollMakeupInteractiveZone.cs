using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DollMakeupInteractiveZone : MonoBehaviour, IDropHandler
{
    public event Action<GameObject> OnDropAction;

    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private RectTransform _targetRectTransform;

    private RectTransform _rectTransform;

    public RectTransform RectTransform => _rectTransform;
     
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        var dragObject = eventData.pointerDrag.gameObject;
        OnDropAction?.Invoke(dragObject);
    }
}