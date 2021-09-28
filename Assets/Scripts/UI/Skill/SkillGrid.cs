using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGrid : GridBase
{
    public override void OnInputTo(ItemBase itemBase)
    {
        GameObject g = Instantiate(itemBase.gameObject);
        g.transform.SetParent(this.transform, false);
        g.transform.localScale = Vector3.one;
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

    public override void OnOutputTo(GridPoint _gridPoint, ItemBase itemBase)
    {
    }

    public override void OnRightKeyBuy()
    {
    }

    public override void OnRightKeyLoading()
    {
    }

    public override void OnRightKeySell()
    {
    }

    public override void OnRightKeyUnload()
    {
    }
}
