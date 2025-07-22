using System;
using UnityEngine;
using UnityEngine.UI;

public class Doll : MonoBehaviour
{
    public event Action OnUseItem;
    public event Action OnReset;

    [SerializeField] private DollMakeupInteractiveZone _interactiveZone;
    [SerializeField] private Loofah _loofah;
    [SerializeField] private Image _acneImage;
    [SerializeField] private Image _eyeShadowMakeupImage;
    [SerializeField] private Image _lipstickhMakeupImage;
    [SerializeField] private Image _blushMakeupImage;

    private MakeupManager _makeupManager;
    private DollAcne _dollAcne;
    private DollEyeShadowMakeup _eyeShadowMakeup;
    private DollBlushMakeup _blushMakeup;
    private DollLipstickMakeup _lipstickMakeup;


    public RectTransform RectTransform => _interactiveZone.RectTransform;

    public void Initialize(MakeupManager makeupManager)
    {
        _makeupManager = makeupManager;

        _interactiveZone.OnDropAction += OnInteractiveZoneDrop;
        _loofah.OnClick += OnLoofahClick;

        _dollAcne = new DollAcne(_acneImage, this, _makeupManager);
        _eyeShadowMakeup = new DollEyeShadowMakeup(_eyeShadowMakeupImage, this, _makeupManager);
        _blushMakeup = new DollBlushMakeup(_blushMakeupImage, this, _makeupManager);
        _lipstickMakeup = new DollLipstickMakeup(_lipstickhMakeupImage, this, _makeupManager);

        OnReset?.Invoke();
    }

    private void OnLoofahClick()
    {
        OnReset?.Invoke();
    }

    private void OnInteractiveZoneDrop(GameObject dropedObject)
    {
        if (dropedObject.TryGetComponent(out Hand hand))
        {
            OnUseItem?.Invoke();
        }
    }
}
