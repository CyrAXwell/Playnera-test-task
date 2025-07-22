public interface IMakeupItemVisitor
{
    void Visit(MakeupItemSO makeupItem);
    
    void Visit(EyeShadowItemSO eyeShadowItem);
    void Visit(LipstickItemSO lipstickItem);
    void Visit(BlushItemSO blushItem);
}