using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
/// <summary>
/// 
/// </summary>
public enum RighhtKeyActionType
{
    NULL=-1,
    Loading = 0,
    Unload = 1,
    Buy = 2,
    Sell = 3
}
/// <summary>
/// 格子
/// </summary>
public abstract class GridBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// 格子当前位置
    /// </summary>
    public GridPoint GridPoint_ = GridPoint.NULL;
    ItemBase itemBase = null;

    public bool IsItemExist
    {
        get
        {
            if (transform.childCount > 0 && transform.GetChild(0).GetComponent<ItemBase>() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public ItemBase GetItemBase
    {
        get
        {
            if(itemBase == null)
            {
                if (IsItemExist)
                {
                    itemBase = transform.GetChild(0).GetComponent<ItemBase>();
                }
            }
            return itemBase;
        }
    }

    /// <summary>
    /// 放进来
    /// </summary>
    public abstract void OnInputTo(ItemBase itemBase);
    /// <summary>
    /// 拿出去
    /// </summary>
    /// <param name="_gridPoint"></param>
    public abstract void OnOutputTo(GridPoint _gridPoint, ItemBase itemBase);
    /// <summary>
    /// 光标进入
    /// </summary>
    public abstract void OnMouseEnterTo();
    /// <summary>
    /// 光标推出
    /// </summary>
    public abstract void OnMouseExitTo();
    /// <summary>
    /// 光标右键点击
    /// </summary>
    public virtual void OnMouseRightClickTo()
    {
        OnMouseExitTo();
    }
    /// <summary>
    /// 装载
    /// </summary>
    public abstract void OnRightKeyLoading();
    /// <summary>
    /// 卸载
    /// </summary>
    public abstract void OnRightKeyUnload();
    /// <summary>
    /// 购买
    /// </summary>
    public abstract void OnRightKeyBuy();
    /// <summary>
    /// 出售
    /// </summary>
    public abstract void OnRightKeySell();

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnMouseEnterTo();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnMouseExitTo();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
       if(eventData.button == PointerEventData.InputButton.Right && transform.childCount > 0 && transform.GetChild(0).GetComponent<ItemBase>() != null)
        {
            OnMouseRightClickTo();
            ItemBase item = transform.GetChild(0).GetComponent<ItemBase>();
            MinItemInfoPopupWindow.GetInstance.OnOpenPopupInfoWindow(GridPoint_,item,this);
        }
    }
}
