using UnityEngine;

public class MakeupBook : MonoBehaviour
{
    [SerializeField] private MakeupContentSO _contentItems;
    [SerializeField] private MakeupBookCategoryButton _eyeShadowButton;
    [SerializeField] private MakeupBookCategoryButton _lipstickButton;
    [SerializeField] private MakeupBookCategoryButton _blushButton;

    private MakeupBookPage _makeupBookPage;
    private MakeupManager _makeupManager;

    public void Initialize(MakeupBookPage makeupBookPage, MakeupManager itemManager)
    {
        _makeupBookPage = makeupBookPage;
        _makeupManager = itemManager;
        OnEyeShadowButtonClick();

        _makeupBookPage.ItemViewClick += OnItemViewClick;
    }

    private void OnEnable()
    {
        _eyeShadowButton.Click += OnEyeShadowButtonClick;
        _lipstickButton.Click += OnLipstickButtonClick;
        _blushButton.Click += OnBlushButtonClick;
    }

    private void OnDisable()
    {
        _eyeShadowButton.Click -= OnEyeShadowButtonClick;
        _lipstickButton.Click -= OnLipstickButtonClick;
        _blushButton.Click -= OnBlushButtonClick;
        _makeupBookPage.ItemViewClick -= OnItemViewClick;
    }

    private void OnEyeShadowButtonClick()
    {
        if (_makeupManager.IsHandActive())
            return;

        _lipstickButton.Unselect();
        _blushButton.Unselect();
        _eyeShadowButton.Select();
        _makeupBookPage.ShowItems(_contentItems.EyeShadowItems);

        _makeupManager.ShowEyeShadowBrush();
    }

    private void OnLipstickButtonClick()
    {
        if (_makeupManager.IsHandActive())
            return;

        _eyeShadowButton.Unselect();
        _blushButton.Unselect();
        _lipstickButton.Select();
        _makeupBookPage.ShowItems(_contentItems.LipstickItems);

        _makeupManager.HideAllBrushes();
    }
    private void OnBlushButtonClick()
    {
        if (_makeupManager.IsHandActive())
            return;

        _lipstickButton.Unselect();
        _eyeShadowButton.Unselect();
        _blushButton.Select();
        _makeupBookPage.ShowItems(_contentItems.BlushItems);

        _makeupManager.ShowBlushBrush();
    }

    private void OnItemViewClick(MakeupItemView itemView)
    {
        MakeupItemViewVisitor visitor = new MakeupItemViewVisitor(_makeupManager, itemView);
        visitor.Visit(itemView.MakeupItem);
    }
}