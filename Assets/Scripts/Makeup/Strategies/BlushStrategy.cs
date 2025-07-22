using System;
using DG.Tweening;
using UnityEngine;

public class BlushStrategy : IHandStrategy
{
    public event Action OnUseItemCompleted;
    public event Action OnPutBack;

    private Hand _hand;
    private Canvas _canvas;
    private BlushBrush _brush;
    private MakeupItemView _itemView;
    private Vector3 _pickUpPosition;
    private bool _isHoldItem;
    private Transform _brushParentContainer;
    private RectTransform _handRectTransform;
    private RectTransform _handStartPosition;

    public BlushStrategy(BlushBrush eyeShadowBrush, Hand hand, Canvas canvas, RectTransform handStartPosition)
    {
        _brush = eyeShadowBrush;
        _hand = hand;
        _canvas = canvas;
        _handStartPosition = handStartPosition;

        _handRectTransform = _hand.GetComponent<RectTransform>();
        _brushParentContainer = _brush.transform.parent;
    }

    public void PickUpTargetItem(MakeupItemView itemView)
    {
        _itemView = itemView;
        float duration = 0.4f;

        Sequence sequence = DOTween.Sequence();
        Vector2 targetPosition;

        if (!_isHoldItem)
        {
            targetPosition = _brush.transform.position - (_hand.BlushWayPoints.PickUpPoint - _hand.transform.position);
            targetPosition = targetPosition / _canvas.scaleFactor;
            _pickUpPosition = targetPosition;
            sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(PickUp));
        }

        _isHoldItem = true;

        duration = 0.4f;
        Vector3 ItemViewPickUpPoint = _itemView.transform.position - (_hand.BlushWayPoints.ItemViewPickUpPoint - _hand.transform.position);
        ItemViewPickUpPoint = ItemViewPickUpPoint / _canvas.scaleFactor;
        sequence.Append(_handRectTransform.DOAnchorPos(ItemViewPickUpPoint, duration));

        RectTransform itemViewRectTransform = _itemView.GetComponent<RectTransform>();
        Vector3 delta = new Vector3(itemViewRectTransform.rect.width / 3, 0, 0);
        float time = 0.15f;
        int moveCount = 4;
        for (int i = 0; i < moveCount; i++)
        {
            targetPosition = ItemViewPickUpPoint + delta;
            sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, time));
            delta *= -1;
        }
        
        duration = 0.5f;
        
        targetPosition = _hand.BlushWayPoints.HoldPoint / _canvas.scaleFactor;
        sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration).
            OnStart(ChangeBrushColor).
            OnComplete(_hand.CanDrag));
    }

    private void ChangeBrushColor()
    {
        BlushItemSO item = (BlushItemSO)_itemView.MakeupItem;
        _brush.ChangeColor(item.TipColor);
    }

    private void PickUp()
    {
        _brush.transform.SetParent(_hand.BlushWayPoints.transform);
    }

    public void UsePickUpItem()
    {
        _hand.StopDrag();
        float duration = 0.2f;
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < 2; i++)
        {
            foreach (var point in _hand.BlushWayPoints.MakeupPoints)
            {
                Vector2 targetPosition = point / _canvas.scaleFactor;
                sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration));
            }
        }
        sequence.OnComplete(PutBack);
    }

    private void PutBack()
    {
        _brush.Clear();
        OnUseItemCompleted?.Invoke();

        float duration = 0.5f;

        Vector2 targetPosition = _pickUpPosition;
        _handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(MoveToDefaultPosition);
    }

    private void MoveToDefaultPosition()
    {
        _brush.transform.SetParent(_brushParentContainer);

        float duration = 0.3f;
        Vector2 targetPosition = _handStartPosition.position / _canvas.scaleFactor;
        _handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(OnHandPutback);
    }

    private void OnHandPutback()
    {
        _isHoldItem = false;
        _hand.gameObject.SetActive(false);
        OnPutBack.Invoke();
    }
}