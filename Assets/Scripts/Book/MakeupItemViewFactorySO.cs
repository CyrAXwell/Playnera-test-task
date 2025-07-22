using UnityEngine;

[CreateAssetMenu(fileName = "MakeupItemViewFactorySO", menuName = "Makeup/MakeupItemViewFactory")]
public class MakeupItemViewFactorySO : ScriptableObject
{
    [SerializeField] private MakeupItemView _eyeShadowItemViewPrefab;
    [SerializeField] private MakeupItemView _lipstickItemViewPrefab;
    [SerializeField] private MakeupItemView _blushItemViewPrefab;

    public MakeupItemView Get(MakeupItemSO makeupItem, Transform eyeShadowItemViewParent, Transform lipstickItemViewParent, Transform blushItemViewParent)
    {
        MakeupItemVisitor visitor = new MakeupItemVisitor(_eyeShadowItemViewPrefab, _lipstickItemViewPrefab, _blushItemViewPrefab, eyeShadowItemViewParent, lipstickItemViewParent, blushItemViewParent);
        visitor.Visit(makeupItem);

        MakeupItemView instance = Instantiate(visitor.Prefab, visitor.Parent);
        instance.Initialize(makeupItem);
        
        return instance;
    }

    private class MakeupItemVisitor : IMakeupItemVisitor
    {
        private MakeupItemView _eyeShadowItemViewPrefab;
        private MakeupItemView _lipstickItemViewPrefab;
        private MakeupItemView _blushItemViewPrefab;

        private Transform _eyeShadowItemViewParent;
        private Transform _lipstickItemViewParent;
        private Transform _blushItemViewParent;

        public MakeupItemVisitor(MakeupItemView eyeShadowItemViewPrefab, MakeupItemView lipstickItemViewPrefab, MakeupItemView blushItemViewPrefab, 
            Transform eyeShadowItemViewParent, Transform lipstickItemViewParent, Transform blushItemViewParent)
        {
            _eyeShadowItemViewPrefab = eyeShadowItemViewPrefab;
            _lipstickItemViewPrefab = lipstickItemViewPrefab;
            _blushItemViewPrefab = blushItemViewPrefab;

            _eyeShadowItemViewParent = eyeShadowItemViewParent;
            _lipstickItemViewParent = lipstickItemViewParent;
            _blushItemViewParent = blushItemViewParent;
        }

        public MakeupItemView Prefab { get; private set; }
        public Transform Parent { get; private set; }

        public void Visit(MakeupItemSO makeupItem)
        {
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
            Prefab = _eyeShadowItemViewPrefab;
            Parent = _eyeShadowItemViewParent;
        } 
            

        public void Visit(LipstickItemSO lipstickItem)
        {
            Prefab = _lipstickItemViewPrefab;
            Parent = _lipstickItemViewParent;
        } 

        public void Visit(BlushItemSO blushItem)
        {
            Prefab = _blushItemViewPrefab;
            Parent = _blushItemViewParent;
        } 
    }
}