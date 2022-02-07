using System;

[Serializable]
public abstract class AbstractItemContainer<T> : UIContainerbase<T>
{
    
    public override void SetContainment(T pContainment)
    {
        base.SetContainment(pContainment);
        UpdateItemContainer();
    }

    public abstract void UpdateItemContainer();
}