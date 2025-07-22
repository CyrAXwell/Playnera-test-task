using UnityEngine;

public class MakeupItemViewVisitor : IMakeupItemVisitor
{
    private MakeupManager _makeupManager;
    private MakeupItemView _itemView;

    public MakeupItemViewVisitor(MakeupManager makeupManager, MakeupItemView itemView)
    {
        _makeupManager = makeupManager;
        _itemView = itemView;
    }

    public void Visit(MakeupItemSO makeupItem)
    {
        if (_makeupManager.IsCreamPickedUp() || _makeupManager.IsUseItem)
            return;

        switch (makeupItem)
        {
            case EyeShadowItemSO eyeShadowItem:
                Visit(eyeShadowItem);
                break;  
            case LipstickItemSO lipstickItem:
                Visit(lipstickItem);
                break;  
            case BlushItemSO blushItem:
                Visit(blushItem);
                break;  
        }
    }

    public void Visit(EyeShadowItemSO eyeShadowItem)
    {
        _makeupManager.OnEyeShadowClick(_itemView);
    }

    public void Visit(LipstickItemSO lipstickItem)
    {
        _makeupManager.OnLipstickClick(_itemView);
    }

    public void Visit(BlushItemSO blushItem)
    {
        _makeupManager.OnBlushClick(_itemView);
    }
}