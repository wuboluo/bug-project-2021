using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.Props
{
    [Serializable]
    public partial class PropAttr
    {
        // ------------------------------------------------------------ 

        [ShowIf("isWeapon")] [Toggle("enable")] [LabelText("攻击")] [SerializeField]
        private Attr_Atk atk;

        [ShowIf("isWeapon")] [Toggle("enable")] [LabelText("防御")] [SerializeField]
        private Attr_Def def;

        [ShowIf("isWeapon")] [Toggle("enable")] [LabelText("生命值")] [SerializeField]
        private Attr_Hp hp;

        [ShowIf("isWeapon")] [Toggle("enable")] [LabelText("移动速度")] [SerializeField]
        private Attr_Speed speed;

        // ------------------------------------------------------------ 
        [HideInInspector] public bool isWeapon;
        public Attr_Atk Atk => atk;
        public Attr_Def Def => def;
        public Attr_Hp Hp => hp;
        public Attr_Speed Speed => speed;
    }

    public partial class PropAttr
    {
        // ------------------------------------------------------------ 

        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("价格")] [SerializeField]
        private Attr_Price price;

        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("稀有度")] [SerializeField]
        private Attr_Rarity rarity;

        public Attr_Price Price => price;
        public Attr_Rarity Rarity => rarity;
    }
}