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
/// ����
/// </summary>
public abstract class GridBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    /// <summary>
    /// ���ӵ�ǰλ��
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
    /// �Ž���
    /// </summary>
    public abstract void OnInputTo(ItemBase itemBase);
    /// <summary>
    /// �ó�ȥ
    /// </summary>
    /// <param name="_gridPoint"></param>
    public abstract void OnOutputTo(GridPoint _gridPoint, ItemBase itemBase);
    /// <summary>
    /// ������
    /// </summary>
    public abstract void OnMouseEnterTo();
    /// <summary>
    /// ����Ƴ�
    /// </summary>
    public abstract void OnMouseExitTo();
    /// <summary>
    /// ����Ҽ����
    /// </summary>
    public virtual void OnMouseRightClickTo()
    {
        OnMouseExitTo();
    }
    /// <summary>
    /// װ��
    /// </summary>
    public abstract void OnRightKeyLoading();
    /// <summary>
    /// ж��
    /// </summary>
    public abstract void OnRightKeyUnload();
    /// <summary>
    /// ����
    /// </summary>
    public abstract void OnRightKeyBuy();
    /// <summary>
    /// ����
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
