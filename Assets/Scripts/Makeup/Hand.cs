using UnityEngine;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Canvas _mainCanvas;
    [SerializeField] private LipstickWayPoints _lipstickWayPoints;
    [SerializeField] private EyeShadowkWayPoints _eyeShadowkWayPoints;
    [SerializeField] private BlushWayPoints _blushWayPoints;
    [SerializeField] private CreamWayPoints _creamWayPoints;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _parentScale;
    private bool _canDrag;

    public LipstickWayPoints LipstickWayPoints => _lipstickWayPoints;
    public EyeShadowkWayPoints EyeShadowkWayPoints => _eyeShadowkWayPoints;
    public BlushWayPoints BlushWayPoints => _blushWayPoints;
    public CreamWayPoints CreamWayPoints => _creamWayPoints;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _parentScale = GetComponentInParent<RectTransform>().localScale;        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_canDrag)
            return;
        _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor * _parentScale;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
    }

    public void CanDrag()
    {
        _canDrag = true;
    }

    public void StopDrag()
    {
        _canDrag = false;
    }
}