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
            MyName.text = item.MyName;
            MyIcon.texture = item.MyIcon;
            MyIntroduce.text = item.MyIntroduce;
            if (item.MyAtk > 0)
            {
                MyAtk.SetActive(true);
                MyAtk.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyAtk.transform.GetChild(1).GetComponent<Text>().text = item.MyAtk.ToString();
            }
            if (item.MyDef > 0)
            {
                MyDef.SetActive(true);
                MyDef.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyDef.transform.GetChild(1).GetComponent<Text>().text = item.MyDef.ToString();
            }
            if (item.MyHp > 0)
            {
                MyHp.SetActive(true);
                MyHp.transform.GetChild(0).GetComponent<Text>().text = "����";
                MyHp.transform.GetChild(1).GetComponent<Text>().text = item.MyHp.ToString();
            }
            if (item.MyMoveSpeed > 0)
            {
                MyMoveSpeed.SetActive(true);
                MyMoveSpeed.transform.GetChild(0).GetComponent<Text>().text = "�ƶ��ٶ�";
                MyMoveSpeed.transform.GetChild(1).GetComponent<Text>().text = item.MyMoveSpeed.ToString();
            }
            this.transform.localScale = Vector3.one;
        }

        public void OnCloseWindow()
        {
            this.transform.localScale = Vector3.zero;
        }



    }
}
