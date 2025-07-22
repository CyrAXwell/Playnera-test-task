using UnityEngine.UI;

public class DollAcne
{
    private Image _acneImage;
    private Doll _doll;
    private MakeupManager _makeupManager;

    public DollAcne(Image image, Doll doll, MakeupManager makeupManager)
    {
        _acneImage = image;
        _doll = doll;
        _makeupManager = makeupManager;

        _doll.OnReset += OnReset;
        _makeupManager.OnCreamUse += OnClear;
    }

    public void OnClear()
    {
        _acneImage.enabled = false;
    }

    public void OnReset()
    {
        _acneImage.enabled = true;
    }
}
