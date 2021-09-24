using System;
using System.Linq;
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

        public bool InitValues()
        {
            var values = new[] {atk.Value, def.Value, hp.Value, speed.Value};
            return !values.All(_ => _.Equals(default));
        }

        public (float _atk, float _def, float _hp, float _speed) GetValues()
        {
            return (atk.Value, def.Value, hp.Value, speed.Value);
        }
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