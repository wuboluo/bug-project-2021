using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// ����Ϣ����
/// </summary>
public class MinItemInfoPopupWindow : ItemInfoPopupWindowBase<MinItemInfoPopupWindow>,IPointerExitHandler
{
    public List<Button> SonWindowLists = new List<Button>();

    public override void OnClosePopupInfoWindow()
    {
        transform.localScale = Vector3.zero;
    }
    public override void OnOpenPopupInfoWindow<Item>(Item item,GridBase GridPoint){ }
    public override void OnOpenPopupInfoWindow(GridPoint gridPoint, ItemBase item,GridBase _backpackGrid)
    {
        SonWindowLists.ForEach(s => s.gameObject.SetActive(false));
        SonWindowLists.ForEach(s => s.onClick.RemoveAllListeners());
        transform.localScale = Vector3.one;
        transform.position = Input.mousePosition;
        switch (gridPoint)
        {
            case GridPoint.NULL:
                break;
            case GridPoint.Backpack:
                SonWindowLists[0].gameObject.SetActive(true);
                //����װ��
                SonWindowLists[0].onClick.AddListener(() =>
                {
                    try
                    {
                        //װ����������
                        GridBase equipGridBase = EquipGridControl.GetInstance.GridBaseLists.Find(s => (s as EquipGrid).EquipType_ == item.EquipType_);
                        if(equipGridBase != null)
                        {
                            if (equipGridBase.transform.childCount <= 0)
                            {
                                //װ�����뱳������=����
                                equipGridBase.OnInputTo(item);
                            }
                            else
                            {
                                ItemBase eq_item = equipGridBase.transform.GetChild(0).GetComponent<ItemBase>();
                                _backpackGrid.OnInputTo(eq_item);
                                equipGridBase.OnInputTo(item);
                            }
                            PlayerInfo.GetInstance.OnUIEventUpdate();
                            OnClosePopupInfoWindow();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        print(ex.Message.ToString());
                        //throw;
                    }
                });

                SonWindowLists[3].gameObject.SetActive(true);
                //����
                SonWindowLists[3].onClick.AddListener(() =>
                {

                });
                break;
            case GridPoint.Equip:
                SonWindowLists[1].gameObject.SetActive(true);
                //ж��װ��
                SonWindowLists[1].onClick.AddListener(() =>
                {
                    try
                    {
                        GridBase gridBase = BackpackGridControl.GetInstance.GridBaseLists.Find(s => s.transform.childCount <= 0);
                        if (gridBase != null)
                        {
                            gridBase.OnInputTo(item);
                            PlayerInfo.GetInstance.OnUIEventUpdate();
                            OnClosePopupInfoWindow();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        print(ex.Message.ToString());
                        //throw;
                    }
                });
                break;
            case GridPoint.Skill:
                break;
            case GridPoint.Store:
                SonWindowLists[2].gameObject.SetActive(true);
                //����װ��
                SonWindowLists[2].onClick.AddListener(() =>
                {
                    try
                    {
                        GridBase gridBase = BackpackGridControl.GetInstance.GridBaseLists.Find(s => s.transform.childCount <= 0);
                        if(gridBase!= null)
                        {
                            GameObject g = Instantiate(item.gameObject);
                            gridBase.OnInputTo(g.GetComponent<ItemBase>());
                            //gridBase.OnInputTo(item);
                            OnClosePopupInfoWindow();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        print(ex.Message.ToString());
                        //throw;
                    }
                });
                break;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnClosePopupInfoWindow();
    }
}
