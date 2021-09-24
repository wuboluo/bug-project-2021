using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            //foreach (Transform g in Group)
            //{
            //    g.gameObject.SetActive(false);
            //}
            //MyName.text = item.DATA.Name;
            //MyIcon.texture = item.DATA.Icon;
            //MyIntroduce.text = item.DATA.Describe;
            //if (item.DATA.Attr.First().Atk > 0)
            //{
            //    MyAtk.SetActive(true);
            //    MyAtk.transform.GetChild(0).GetComponent<Text>().text = "����";
            //    MyAtk.transform.GetChild(1).GetComponent<Text>().text = item.DATA.Attr.First().Atk.ToString();
            //}
            //if (item.DATA.Attr.First().Def > 0)
            //{
            //    MyDef.SetActive(true);
            //    MyDef.transform.GetChild(0).GetComponent<Text>().text = "����";
            //    MyDef.transform.GetChild(1).GetComponent<Text>().text = item.DATA.Attr.First().Def.ToString();
            //}
            //if (item.DATA.Attr.First().Hp > 0)
            //{
            //    MyHp.SetActive(true);
            //    MyHp.transform.GetChild(0).GetComponent<Text>().text = "����";
            //    MyHp.transform.GetChild(1).GetComponent<Text>().text = item.DATA.Attr.First().Hp.ToString();
            //}
            //if (item.DATA.Attr.First().Speed > 0)
            //{
            //    MyMoveSpeed.SetActive(true);
            //    MyMoveSpeed.transform.GetChild(0).GetComponent<Text>().text = "�ƶ��ٶ�";
            //    MyMoveSpeed.transform.GetChild(1).GetComponent<Text>().text = item.DATA.Attr.First().Speed.ToString();
            //}
            //this.transform.localScale = Vector3.one;
        }

        public void OnCloseWindow()
        {
            this.transform.localScale = Vector3.zero;
        }



    }
}
