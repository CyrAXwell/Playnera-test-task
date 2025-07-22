using UnityEngine;
using UnityEngine.UI;

public class BlushBrush : MonoBehaviour
{
    [SerializeField] Image _tip;

    public void ChangeColor(Color color)
    {
        _tip.enabled = true;
        _tip.color = color;
    } 

    public void Clear()
    {
        _tip.enabled = false;
    }

    private void OnEnable()
    {
        Clear();
    }
}