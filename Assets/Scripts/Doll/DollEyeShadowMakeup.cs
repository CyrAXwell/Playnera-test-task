using UnityEngine;
using UnityEngine.UI;

public class DollEyeShadowMakeup
{
    private Image _eyeShadowImage;
    private Doll _doll;
    private MakeupManager _makeupManager;

    public DollEyeShadowMakeup(Image image, Doll doll, MakeupManager makeupManager)
    {
        _eyeShadowImage = image;
        _doll = doll;
        _makeupManager = makeupManager;

        _doll.OnReset += OnReset;
        _makeupManager.OnEyeShadowMakeup += OnMakeup;
    }

    public void OnMakeup(MakeupItemView itemView)
    {
        _eyeShadowImage.enabled = true;
        _eyeShadowImage.sprite = itemView.MakeupItem.MakeupSprite;
    }

    public void OnReset()
    {
        _eyeShadowImage.enabled = false;
    }
}
