using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "BlushItemSO", menuName = "Makeup/BlushItem") ]
public class BlushItemSO : MakeupItemSO
{
    [field: SerializeField] public Color TipColor { get; private set; } 
}