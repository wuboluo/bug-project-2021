using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bug.Project21.Backpack
{
    public class ItemInfoPopupWindow : MonoBehaviour
    {
        public Transform Group = null;
        /// <summary>
        /// 名称
        /// </summary>
        public Text MyName = null;
        /// <summary>
        /// 图标
        /// </summary>
        public RawImage MyIcon = null;
        /// <summary>
        /// 说明
        /// </summary>
        public Text MyIntroduce = null;
        /// <summary>
        /// 攻击
        /// </summary>
        public GameObject MyAtk = null;
        /// <summary>
        /// 防御
        /// </summary>
        public GameObject MyDef = null;
        /// <summary>
        /// 体力
        /// </summary>
        public GameObject MyHp = null;
        /// <summary>
        /// 移动速度
        /// </summary>
        public GameObject MyMoveSpeed = null;
        public void OnOpenWindow(Item item)
        {
            foreach (Transform g in Group)
            {
                g.gameObject.SetActive(false);
            }

            (float _atk, float _def, float _hp, float _speed) values = item.dataSO.Attrs.GetValues();
            

            MyName.text = item.dataSO.Name;
            MyIcon.texture = item.dataSO.Icon;
            MyIntroduce.text = item.dataSO.Describe;
            if (values._atk > 0)
            {
                MyAtk.SetActive(true);
                MyAtk.transform.GetChild(0).GetComponent<Text>().text = "攻击";
                MyAtk.transform.GetChild(1).GetComponent<Text>().text = values._atk.ToString();
            }
            if (values._def > 0)
            {
                MyDef.SetActive(true);
                MyDef.transform.GetChild(0).GetComponent<Text>().text = "防御";
                MyDef.transform.GetChild(1).GetComponent<Text>().text = values._def.ToString();
            }
            if (values._hp > 0)
            {
                MyHp.SetActive(true);
                MyHp.transform.GetChild(0).GetComponent<Text>().text = "生命";
                MyHp.transform.GetChild(1).GetComponent<Text>().text = values._hp.ToString();
            }
            if (values._speed > 0)
            {
                MyMoveSpeed.SetActive(true);
                MyMoveSpeed.transform.GetChild(0).GetComponent<Text>().text = "移动速度";
                MyMoveSpeed.transform.GetChild(1).GetComponent<Text>().text = values._speed.ToString();
            }
            this.transform.localScale = Vector3.one;
        }

        public void OnCloseWindow()
        {
            this.transform.localScale = Vector3.zero;
        }



    }
}
