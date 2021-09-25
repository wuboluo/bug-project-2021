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
        /// ����
        /// </summary>
        public Text MyName = null;
        /// <summary>
        /// ͼ��
        /// </summary>
        public RawImage MyIcon = null;
        /// <summary>
        /// ˵��
        /// </summary>
        public Text MyIntroduce = null;
        /// <summary>
        /// ����
        /// </summary>
        public GameObject MyAtk = null;
        /// <summary>
        /// ����
        /// </summary>
        public GameObject MyDef = null;
        /// <summary>
        /// ����
        /// </summary>
        public GameObject MyHp = null;
        /// <summary>
        /// �ƶ��ٶ�
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
                MyAtk.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyAtk.transform.GetChild(1).GetComponent<Text>().text = values["atk"].ToString();
            }
            if (is_values["def"])
            {
                MyDef.SetActive(true);
                MyDef.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyDef.transform.GetChild(1).GetComponent<Text>().text = values["def"].ToString();
            }
            if (is_values["hp"])
            {
                MyHp.SetActive(true);
                MyHp.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyHp.transform.GetChild(1).GetComponent<Text>().text = values["hp"].ToString();
            }
            if (is_values["speed"])
            {
                MyMoveSpeed.SetActive(true);
                MyMoveSpeed.transform.GetChild(0).GetComponent<Text>().text = "�ƶ��ٶ�";
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
