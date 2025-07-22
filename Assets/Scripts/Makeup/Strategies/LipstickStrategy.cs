using System;
using DG.Tweening;
using UnityEngine;

public class LipstickStrategy : IHandStrategy
{
    public event Action OnUseItemCompleted;
    public event Action OnPutBack;

    private Hand _hand;
    private Canvas _canvas;
    private MakeupItemView _itemView;
    private RectTransform _handRectTransform;
    private RectTransform _handStartPosition;
    private Vector3 _pickUpPosition;
    private Transform _itemViewParentConatiner;
    private bool _isHoldItem;

    public LipstickStrategy(Hand hand, Canvas canvas, RectTransform handStartPosition)
    {
        _hand = hand;
        _canvas = canvas;
        _handStartPosition = handStartPosition;

        _handRectTransform = _hand.GetComponent<RectTransform>();
    }

    public void PickUpTargetItem(MakeupItemView itemView)
    {
        if (_isHoldItem)
        {
            ChangeColor(itemView);
            return;
        }

        _isHoldItem = true;
        _itemView = itemView;
        _itemViewParentConatiner = _itemView.transform.parent;

        float duration = 0.6f;
        Sequence sequence = DOTween.Sequence();

        Vector2 targetPosition = _itemView.transform.position - (_hand.LipstickWayPoints.PickUpPoint - _hand.transform.position);
        targetPosition = targetPosition / _canvas.scaleFactor;
        _pickUpPosition = targetPosition;
        sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(PickUp));

        duration = 0.5f;
        targetPosition = _hand.LipstickWayPoints.HoldPoint / _canvas.scaleFactor;
        sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(_hand.CanDrag));
    }

    private void PickUp()
    {
        _itemView.gameObject.transform.SetParent(_hand.LipstickWayPoints.transform);
    }

    public void UsePickUpItem()
    {
        _hand.StopDrag();
        float duration = 0.2f;
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < 2; i++)
        {
            foreach (Vector3 point in _hand.LipstickWayPoints.MakeupPoints)
            {
                Vector2 targetPosition = point / _canvas.scaleFactor;
                sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration));
            }
        }
        sequence.OnComplete(PutBack);
    }

    private void PutBack()
    {
        OnUseItemCompleted?.Invoke();

        float duration = 0.5f;
        Vector2 targetPosition = _pickUpPosition;
        _handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(MoveToDefaultPosition);
    }

    private void MoveToDefaultPosition()
    {
        _itemView.transform.SetParent(_itemViewParentConatiner);

        float duration = 0.4f;
        Vector2 targetPosition = _handStartPosition.position / _canvas.scaleFactor;
        _handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(OnHandPutback);
    }

    private void OnHandPutback()
    {
        _hand.gameObject.SetActive(false);
        _isHoldItem = false;
        OnPutBack.Invoke();
    }

    private void ChangeColor(MakeupItemView itemView)
    {
        float duration = 0.5f;

        Vector2 targetPosition = _pickUpPosition;
        _handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(() => {
            _itemView.transform.SetParent(_itemViewParentConatiner);
            _isHoldItem = false;
            PickUpTargetItem(itemView);
        });
    }
}