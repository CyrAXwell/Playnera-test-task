using System;

public interface IHandStrategy
{
    public event Action OnUseItemCompleted;
    public event Action OnPutBack;

    public void PickUpTargetItem(MakeupItemView itemView);

    public void UsePickUpItem();
}