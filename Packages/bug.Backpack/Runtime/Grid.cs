using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Bug.Project21.Backpack
{
    /// <summary>
    /// 装备槽位置
    /// 1：背包槽
    /// 2：装备槽
    /// 3：技能槽
    /// 4：商店槽
    /// </summary>
    public enum GridPoint { NULL = -1, BackpackPoint = 0, EquipPoint = 1, SkillPoint = 2, StorePoint = 3 }
    /// <summary>
    /// 物品槽
    /// </summary>
    public class Grid : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        public GridPoint MyGridPoint = GridPoint.NULL;
        private ItemInfoPopupWindow _itemInfoPopupWindow = null;
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (this.transform.childCount > 0)
            {
                Item _item = this.transform.GetChild(0).GetComponent<Item>();
                _itemInfoPopupWindow.OnOpenWindow(_item);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _itemInfoPopupWindow.OnCloseWindow();
        }

        void Start()
        {
            this._itemInfoPopupWindow = FindObjectOfType<ItemInfoPopupWindow>();
        }

        void Update()
        {
        
        }
    }
}
