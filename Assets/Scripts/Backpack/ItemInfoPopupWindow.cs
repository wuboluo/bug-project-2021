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

            //(float _atk, float _def, float _hp, float _speed) values = item.dataSO.Attrs.GetValues(Props.PropTag.weapon);

            Dictionary<string,int> values = item.dataSO.Attrs.GetValues(Props.PropTag.weapon);
            Dictionary<string,bool> is_values = item.dataSO.Attrs.GetAbles(Props.PropTag.weapon);


            MyName.text = item.dataSO.Name;
            MyIcon.texture = item.dataSO.Icon;
            MyIntroduce.text = item.dataSO.Describe;
            //if (values["atk"] > 0)
            if(is_values["atk"])
            {
                MyAtk.SetActive(true);
                MyAtk.transform.GetChild(0).GetComponent<Text>().text = "攻击";
                MyAtk.transform.GetChild(1).GetComponent<Text>().text = values["atk"].ToString();
            }
            if (is_values["def"])
            {
                MyDef.SetActive(true);
                MyDef.transform.GetChild(0).GetComponent<Text>().text = "防御";
                MyDef.transform.GetChild(1).GetComponent<Text>().text = values["def"].ToString();
            }
            if (is_values["hp"])
            {
                MyHp.SetActive(true);
                MyHp.transform.GetChild(0).GetComponent<Text>().text = "生命";
                MyHp.transform.GetChild(1).GetComponent<Text>().text = values["hp"].ToString();
            }
            if (is_values["speed"])
            {
                MyMoveSpeed.SetActive(true);
                MyMoveSpeed.transform.GetChild(0).GetComponent<Text>().text = "移动速度";
                MyMoveSpeed.transform.GetChild(1).GetComponent<Text>().text = values["speed"].ToString();
            }
            this.transform.localScale = Vector3.one;
        }

        public void OnCloseWindow()
        {
            this.transform.localScale = Vector3.zero;
        }



    }
}
