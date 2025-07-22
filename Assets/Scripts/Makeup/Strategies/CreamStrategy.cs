using System;
using DG.Tweening;
using UnityEngine;

public class CreamStrategy : IHandStrategy
{
    public event Action OnUseItemCompleted;
    public event Action OnPutBack;

    private Cream _cream;
    private Hand _hand;
    private Canvas _canvas;
    private Vector3 _pickUpPosition;
    private Transform _creamParentContainer;
    
    private RectTransform _handRectTransform;
    private RectTransform _handStartPosition;

    public CreamStrategy(Cream cream, Hand hand, Canvas canvas, RectTransform handStartPosition)
    {
        _cream = cream;
        _hand = hand;
        _canvas = canvas;
        _handStartPosition = handStartPosition;

        _handRectTransform = _hand.GetComponent<RectTransform>();
        _creamParentContainer = _cream.transform.parent;
    }

    public void PickUpTargetItem(MakeupItemView itemView)
    {
        float duration = 0.9f;

        Sequence sequence = DOTween.Sequence();

        Vector2 targetPosition = _cream.transform.position - (_hand.CreamWayPoints.PickUpPoint - _hand.transform.position);
        targetPosition = targetPosition / _canvas.scaleFactor;
        _pickUpPosition = targetPosition;
        sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(PickUpCream));

        duration = 0.3f;
        targetPosition = _hand.CreamWayPoints.HoldPoint / _canvas.scaleFactor;
        sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(_hand.CanDrag));
    }

    private void PickUpCream()
    {
        _cream.transform.SetParent(_hand.CreamWayPoints.transform);
    }

    public void UsePickUpItem()
    {
        _hand.StopDrag();
        float duration = 0.2f;

        Sequence sequence = DOTween.Sequence();
        foreach (var point in _hand.CreamWayPoints.MakeupPoints)
        {
            Vector2 targetPosition = point / _canvas.scaleFactor;
            sequence.Append(_handRectTransform.DOAnchorPos(targetPosition, duration));
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
        _cream.transform.SetParent(_creamParentContainer);

        float duration = 0.7f;
        Vector2 targetPosition = _handStartPosition.position / _canvas.scaleFactor;
        _handRectTransform.DOAnchorPos(targetPosition, duration).OnComplete(OnHandPutback);
    }

    private void OnHandPutback()
    {
        _cream.Putback();
        _hand.gameObject.SetActive(false);
        OnPutBack.Invoke();
    }
}