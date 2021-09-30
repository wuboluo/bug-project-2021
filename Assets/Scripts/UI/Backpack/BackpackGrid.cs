using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackGrid : GridBase
{
    public override void OnInputTo(ItemBase itemBase)
    {
        itemBase.transform.SetParent(this.transform, false);
    }

    public override void OnOutputTo(GridPoint _gridPoint, ItemBase itemBase)
    {
        
    }

    public override void OnMouseEnterTo()
    {
        if (transform.childCount > 0 && transform.GetChild(0).GetComponent<ItemBase>() != null)
        {
            MaxItemInfoPopupWindow.GetInstance.OnOpenPopupInfoWindow<ItemBase>(transform.GetChild(0).GetComponent<ItemBase>(), this);
        }
    }

    public override void OnMouseExitTo()
    {
        MaxItemInfoPopupWindow.GetInstance.OnClosePopupInfoWindow();
    }

    public override void OnMouseRightClickTo()
    {
        base.OnMouseRightClickTo();
    }

    public override void OnRightKeyLoading()
    {
        
    }

    public override void OnRightKeyUnload()
    {
        
    }

    public override void OnRightKeyBuy()
    {
        
    }

    public override void OnRightKeySell()
    {
        
    }
}