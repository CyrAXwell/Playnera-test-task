using UnityEngine;

public abstract class MakeupItemSO : ScriptableObject
{
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Sprite MakeupSprite { get; private set; }
}
