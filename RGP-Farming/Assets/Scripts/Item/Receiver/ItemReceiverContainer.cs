using UnityEngine;

public class ItemReceiverContainer : ItemReceivedContainment<Item>
{
    public override void SetContainment(Item containment)
    {
        base.SetContainment(containment);
        UpdateItemContainer();
    }

    private void UpdateItemContainer()
    {
        if (Containment == null || Containment.item == null)
        {
            base.ClearContainment();
            return;
        }

        Icon.sprite = Containment.item.uiSprite;
        Icon.enabled = true;

        ItemName.text = $"{Containment.item.itemName}";
        
        Amount.text = $"{Containment.amount}";
        Amount.enabled = true;

        scaleUpdater = 0.5f;
    }

    private float scaleUpdater;
    private void Update()
    {
        if (scaleUpdater > 0) scaleUpdater -= Time.deltaTime;
        
        if (scaleUpdater > 0.25f)
        {
            UpdateScale(0.002f);
        } else if (scaleUpdater > 0)
        {
            UpdateScale(-0.002f);
        }
        else
        {
            Icon.transform.localScale = new Vector3(1, 1 ,1);
        }
    }

    private void UpdateScale(float adjuster)
    {
        var icon = Icon.transform;
        var localScale = icon.localScale;
        localScale = new Vector3(localScale.x + adjuster, localScale.y + adjuster, localScale.y);
        icon.localScale = localScale;
    }
}
