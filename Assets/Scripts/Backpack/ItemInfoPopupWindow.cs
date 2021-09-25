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

            (float _atk, float _def, float _hp, float _speed) values = item.dataSO.Attrs.GetValues();
            

            MyName.text = item.dataSO.Name;
            MyIcon.texture = item.dataSO.Icon;
            MyIntroduce.text = item.dataSO.Describe;
            if (values._atk > 0)
            {
                MyAtk.SetActive(true);
                MyAtk.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyAtk.transform.GetChild(1).GetComponent<Text>().text = values._atk.ToString();
            }
            if (values._def > 0)
            {
                MyDef.SetActive(true);
                MyDef.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyDef.transform.GetChild(1).GetComponent<Text>().text = values._def.ToString();
            }
            if (values._hp > 0)
            {
                MyHp.SetActive(true);
                MyHp.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyHp.transform.GetChild(1).GetComponent<Text>().text = values._hp.ToString();
            }
            if (values._speed > 0)
            {
                MyMoveSpeed.SetActive(true);
                MyMoveSpeed.transform.GetChild(0).GetComponent<Text>().text = "�ƶ��ٶ�";
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
