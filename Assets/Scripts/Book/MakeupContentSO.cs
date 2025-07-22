using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MakeupContentSO", menuName = "Makeup/MakeupContent")]
public class MakeupContentSO : ScriptableObject
{
    [SerializeField] private List<EyeShadowItemSO> _eyeShadowItems;
    [SerializeField] private List<LipstickItemSO> _lipstickItems;
    [SerializeField] private List<BlushItemSO> _blushItems;

    public IEnumerable<EyeShadowItemSO> EyeShadowItems => _eyeShadowItems;
    public IEnumerable<LipstickItemSO> LipstickItems => _lipstickItems;
    public IEnumerable<BlushItemSO> BlushItems => _blushItems;
}