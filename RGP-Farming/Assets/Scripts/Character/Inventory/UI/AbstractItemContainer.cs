using UnityEngine;

public abstract class AbstractItemContainer<T> : UIContainerbase<T>
{
    
    public override void SetContainment(T containment)
    {
        base.SetContainment(containment);
        UpdateItemContainer();
    }

    public abstract void UpdateItemContainer();
}