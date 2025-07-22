using UnityEngine;

[CreateAssetMenu(fileName = "EyeShadowItemSO", menuName = "Makeup/EyeShadow") ]
public class EyeShadowItemSO : MakeupItemSO
{
    [field: SerializeField] public Color TipColor { get; private set; } 
}