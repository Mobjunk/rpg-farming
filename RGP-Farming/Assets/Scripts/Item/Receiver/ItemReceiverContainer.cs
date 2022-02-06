using UnityEngine;

public class ItemReceiverContainer : ItemReceivedContainment<GameItem>
{
    public override void SetContainment(GameItem pContainment)
    {
        base.SetContainment(pContainment);
        UpdateItemContainer();
    }

    private void UpdateItemContainer()
    {
        if (Containment == null || Containment.Item == null)
        {
            base.ClearContainment();
            return;
        }

        Icon.sprite = Containment.Item.uiSprite;
        Icon.enabled = true;

        ItemName.text = $"{Containment.Item.itemName}";
        
        Amount.text = $"{Containment.Amount}";
        Amount.enabled = true;

        _scaleUpdater = 0.5f;
    }

    private float _scaleUpdater;
    private void Update()
    {
        if (_scaleUpdater > 0) _scaleUpdater -= Time.deltaTime;
        
        if (_scaleUpdater > 0.25f)
        {
            UpdateScale(0.002f);
        } else if (_scaleUpdater > 0)
        {
            UpdateScale(-0.002f);
        }
        else
        {
            Icon.transform.localScale = new Vector3(1, 1 ,1);
        }
    }

    private void UpdateScale(float pDdjuster)
    {
        var icon = Icon.transform;
        var localScale = icon.localScale;
        localScale = new Vector3(localScale.x + pDdjuster, localScale.y + pDdjuster, localScale.y);
        icon.localScale = localScale;
    }
}
