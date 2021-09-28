using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 主信息弹窗
/// </summary>
public class MaxItemInfoPopupWindow : ItemInfoPopupWindowBase<MaxItemInfoPopupWindow>
{
    public List<VerticalLayoutGroup> LayoutGroups = new List<VerticalLayoutGroup>();
    public List<GameObject> ValidLists = new List<GameObject>();
    public RawImage MainIcon = null;
    public Text MainNameText = null;
    public Text MainDesText = null;
    public Text AtkText = null;
    public Text DefText = null;
    public Text HPText = null;
    public Text SpeedText = null;

    public GameObject SkillActive = null;
    public RawImage SkillActiveIcon = null;
    public Text SkillActiveNameText = null;
    public Text SkillActiveValueText = null;
    public Text SkillActiveDesText = null;

    public GameObject SkillPassive = null;
    public RawImage SkillPassiveIcon = null;
    public Text SkillPassiveNameText = null;
    public Text SkillPassiveValueText = null;
    public Text SkillPassiveDesText = null;

    public GameObject CostPanel = null;
    public Text CostText = null;

    public override void OnClosePopupInfoWindow()
    {
        transform.localScale = Vector3.zero;
    }

    public override void OnOpenPopupInfoWindow<Item>(Item _item, GridBase _grid)
    {
        LayoutGroups.ForEach(s => s.enabled = false);
        ValidLists.ForEach(s => s.SetActive(false));

        float popupWindow_W = GetComponent<RectTransform>().rect.width;
        float popupWindow_H = GetComponent<RectTransform>().rect.height;
        float grid_W = _grid.GetComponent<RectTransform>().rect.width;
        float grid_H = _grid.GetComponent<RectTransform>().rect.height;

        float offset_x = (popupWindow_W / 2f + grid_W / 2f);

        #region X坐标
        float get_X = _grid.transform.position.x;
        get_X = _grid.transform.position.x - offset_x;
        //get_X = get_X > Screen.width / 2f ? GridPoint.position.x - offset_x : GridPoint.position.x + offset_x;

        if (get_X - popupWindow_W / 2f < 0f || get_X + popupWindow_W / 2f > Screen.width)
        {
            if (get_X - popupWindow_W / 2f < 0f)
            {
                get_X = get_X - popupWindow_W / 2f < 0f ? _grid.transform.position.x + offset_x : _grid.transform.position.x - offset_x;
            }
            if (get_X + popupWindow_W / 2f > Screen.width)
            {
                get_X = get_X + popupWindow_W / 2f > Screen.width ? _grid.transform.position.x - offset_x : _grid.transform.position.x + offset_x;
            }
        }
        #endregion

        #region Y坐标
        float get_Y = _grid.transform.position.y;
        get_Y = get_Y - popupWindow_H / 2f < 0 ? popupWindow_H / 2 : get_Y;
        get_Y = get_Y + popupWindow_H / 2f > Screen.height ? Screen.height - popupWindow_H / 2 : get_Y;
        #endregion

        transform.position = new Vector3(get_X, get_Y, _grid.transform.position.z);
        transform.localScale = Vector3.one;

        ItemBase temp_item = _item as ItemBase;

        MainIcon.texture = temp_item.Icon_;
        MainNameText.text = temp_item.Name_;
        MainDesText.text = "介绍："+temp_item.Des_;

        if (temp_item.Atk_ > 0)
        {
            AtkText.gameObject.SetActive(true);
            AtkText.text = "攻击：" + temp_item.Atk_.ToString();
        }
        if (temp_item.Def_ > 0)
        {
            DefText.gameObject.SetActive(true);
            DefText.text = "防御：" + temp_item.Def_.ToString();
        }
        if (temp_item.HP_ > 0)
        {
            HPText.gameObject.SetActive(true);
            HPText.text = "生命：" + temp_item.HP_.ToString();
        }
        if (temp_item.Speed_ > 0)
        {
            SpeedText.gameObject.SetActive(true);
            SpeedText.text = "移动速度：" + temp_item.Speed_.ToString();
        }
        if (temp_item.SkillActive_ != null)
        {
            SkillActive.SetActive(true);
            SkillActiveIcon.texture = temp_item.SkillActive_.Icon_;
            SkillActiveNameText.text = temp_item.SkillActive_.Name_;
            SkillActiveValueText.text = "增幅属性："+temp_item.SkillActive_.SkillValue_.ToString();
            SkillActiveDesText.text = "介绍："+temp_item.SkillActive_.Des_;
        }
        if (temp_item.SkillPassive_ != null)
        {
            SkillPassive.SetActive(true);
            SkillPassiveIcon.texture = temp_item.SkillPassive_.Icon_;
            SkillPassiveNameText.text = temp_item.SkillPassive_.Name_;
            SkillPassiveValueText.text = "增幅属性：" + temp_item.SkillPassive_.SkillValue_.ToString();
            SkillPassiveDesText.text = "介绍："+temp_item.SkillPassive_.Des_;
        }
        if (temp_item.Cost > 0)
        {
            CostPanel.SetActive(true);
            CostText.text = "价值："+temp_item.Cost.ToString();
        }
        switch (_grid.GridPoint_)
        {
            case global::GridPoint.NULL:
                break;
            case global::GridPoint.Backpack:
                break;
            case global::GridPoint.Equip:
                break;
            case global::GridPoint.Skill:
                break;
            case global::GridPoint.Store:
                break;
        }

        LayoutGroups.ForEach(s => s.enabled = true);
    }

    public override void OnOpenPopupInfoWindow(GridPoint gridPoint, ItemBase item,GridBase grid)
    {
        
    }

    private void Update()
    {
    }
}
