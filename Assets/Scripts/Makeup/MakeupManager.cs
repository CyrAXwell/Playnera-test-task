using System;
using UnityEngine;

public class MakeupManager : MonoBehaviour
{
    public event Action OnCreamUse;
    public event Action<MakeupItemView> OnEyeShadowMakeup;
    public event Action<MakeupItemView> OnBlushMakeup;
    public event Action<MakeupItemView> OnLipstickMakeup;

    [SerializeField] private RectTransform _handStartPosition;
    [SerializeField] private Cream _cream;
    [SerializeField] private EyeShadowBrush _eyeShadowBrush;
    [SerializeField] private BlushBrush _blushBrush;
    [SerializeField] private Doll _doll;
    [SerializeField] private Canvas _canvas;

    private Hand _hand;
    private IHandStrategy _currentHandStrategy;
    private MakeupItemView _makeupItemView;
    private LipstickStrategy _lipstickStrategy;
    private EyeShadowState _eyeShadowStrategy;
    private BlushStrategy _blushStrategy;
    private CreamStrategy _creamStrategy;
    private bool _isUseItem;

    public bool IsUseItem => _isUseItem;

    public void Initialize(Hand hand)
    {
        _hand = hand;
        _hand.transform.position = _handStartPosition.position;
        _hand.gameObject.SetActive(false);

        _cream.Click += OnCreamClick;
        _doll.OnUseItem += OnUseItem;

        _creamStrategy = new CreamStrategy(_cream, _hand, _canvas, _handStartPosition);
        _creamStrategy.OnUseItemCompleted += OnCreamUseCompleted;
        _creamStrategy.OnPutBack += OnPutBack;

        _eyeShadowStrategy = new EyeShadowState(_eyeShadowBrush, _hand, _canvas, _handStartPosition);
        _eyeShadowStrategy.OnUseItemCompleted += OnEyeShadowMakeupCompleted;
        _eyeShadowStrategy.OnPutBack += OnPutBack;

        _blushStrategy = new BlushStrategy(_blushBrush, _hand, _canvas, _handStartPosition);
        _blushStrategy.OnUseItemCompleted += OnBlushMakeupCompleted;
        _blushStrategy.OnPutBack += OnPutBack;

        _lipstickStrategy = new LipstickStrategy(_hand, _canvas, _handStartPosition);
        _lipstickStrategy.OnUseItemCompleted += OnLipstickMakeupCompleted;
        _lipstickStrategy.OnPutBack += OnPutBack;
    }

    public bool IsHandActive()
    {
        return _hand.gameObject.activeInHierarchy;
    }

    public bool IsCreamPickedUp()
    {
        return _cream.IsPickedUp;
    }

    private void Showhand()
    {
        if (IsHandActive())
            return;
        _hand.gameObject.SetActive(true);
    }

    private void OnCreamClick()
    {
        if(IsHandActive())
            return;

        _cream.PickedUp();
        _currentHandStrategy = _creamStrategy;
        
        Showhand();
        _currentHandStrategy.PickUpTargetItem(null);
    }

    public void OnEyeShadowClick(MakeupItemView itemView)
    {
        _makeupItemView = itemView;
        _currentHandStrategy = _eyeShadowStrategy;
        Showhand();
        _currentHandStrategy.PickUpTargetItem(itemView);
    }

    public void OnBlushClick(MakeupItemView itemView)
    {
        _makeupItemView = itemView;
        _currentHandStrategy = _blushStrategy;
        Showhand();
        _currentHandStrategy.PickUpTargetItem(itemView);
    }

    public void OnLipstickClick(MakeupItemView itemView)
    {
        _makeupItemView = itemView;
        _currentHandStrategy = _lipstickStrategy;
        
        Showhand();
        _currentHandStrategy.PickUpTargetItem(itemView);
    }

    private void OnCreamUseCompleted()
    {
        OnCreamUse?.Invoke();
    }

    private void OnEyeShadowMakeupCompleted()
    {
        OnEyeShadowMakeup?.Invoke(_makeupItemView);
    }

    private void OnBlushMakeupCompleted()
    {
        OnBlushMakeup?.Invoke(_makeupItemView);
    }

    private void OnLipstickMakeupCompleted()
    {
        OnLipstickMakeup?.Invoke(_makeupItemView);
    }

    private void OnPutBack()
    {
        _isUseItem = false;
        _currentHandStrategy = null;
    }

    private void OnUseItem()
    {
        _isUseItem = true;
        _currentHandStrategy.UsePickUpItem();
    }

    public void ShowEyeShadowBrush()
    {
        HideAllBrushes();
        _eyeShadowBrush.gameObject.SetActive(true);
    }

    public void ShowBlushBrush()
    {
        HideAllBrushes();
        _blushBrush.gameObject.SetActive(true);
    }

    public void HideAllBrushes()
    {
        _eyeShadowBrush.gameObject.SetActive(false);
        _blushBrush.gameObject.SetActive(false);
    }
}