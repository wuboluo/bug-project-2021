using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bug.Project21.Props;

namespace Bug.Project21.Backpack
{
    /// <summary>
    /// ��Ʒ����
    /// 1��װ��
    /// 2������
    /// </summary>
    public enum ItemType { NULL = -1, Equip = 0, Materials = 1 }
    /// <summary>
    /// װ������
    /// 1������
    /// 2������
    /// 3��Ь��
    /// </summary>
    public enum EquipType { NULL = -1, Weapon = 0, Barde = 1, Shoe = 2 }
    /// <summary>
    /// ��������
    /// 1��
    /// </summary>
    public enum MaterialsType { NULL = -1 }
    /// <summary>
    /// 
    /// </summary>
    public class Item : MonoBehaviour
    {

        public PropDataSO dataSO = null;
        public void Start()
        {
            if(dataSO.Attrs.InitValues(PropTag.weapon)== false)
            {
                dataSO.Attrs.SetValues(PropTag.weapon);
            }
        }

        ///// <summary>
        ///// ��ƷID����ţ�
        ///// </summary>
        //public int ID = 0;
        ///// <summary>
        ///// ��Ʒ����
        ///// </summary>
        //public ItemType MyItemType = ItemType.NULL;
        ///// <summary>
        ///// ��������
        ///// </summary>
        //public EquipType MyEquipType = EquipType.NULL;
        ///// <summary>
        ///// ����
        ///// </summary>
        //public string MyName = string.Empty;
        ///// <summary>
        ///// ͼ��
        ///// </summary>
        //public Texture2D MyIcon = null;
        ///// <summary>
        ///// ˵��
        ///// </summary>
        //public string MyIntroduce = string.Empty;
        ///// <summary>
        ///// ����
        ///// </summary>
        //public int MyAtk = 0;
        ///// <summary>
        ///// ����
        ///// </summary>
        //public int MyDef = 0;
        ///// <summary>
        ///// ����
        ///// </summary>
        //public int MyHp = 0;
        ///// <summary>
        ///// �ƶ��ٶ�
        ///// </summary>
        //public int MyMoveSpeed = 0;
    }
}
