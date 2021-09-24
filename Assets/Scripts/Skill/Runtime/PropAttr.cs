using System;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.Props
{
    [Serializable]
    public partial class PropAttr
    {
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

        /// <summary>
        ///     判断道具属性的值是否都被设置过
        /// </summary>
        /// <returns>true：全部被设置</returns>
        public bool InitValues()
        {
            var values = new[] {atk.Value, def.Value, hp.Value, speed.Value};
            return !values.All(_ => _.Equals(default));
        }

        /// <summary>
        ///     设置道具属性的数值（范围随机）
        /// </summary>
        public void SetValues()
        {
            atk.InitValue();
            def.InitValue();
            hp.InitValue();
            speed.InitValue();
        }

        /// <summary>
        ///     获取道具属性的值
        /// </summary>
        /// <returns></returns>
        public (float _atk, float _def, float _hp, float _speed) GetValues()
        {
            return (atk.Value, def.Value, hp.Value, speed.Value);
        }

        /// <summary>
        ///     获取道具属性是否可使用
        /// </summary>
        /// <returns></returns>
        public (bool _atkAble, bool _defAble, bool _hpAble, bool _speedAble) GetAbles()
        {
            return (atk.enable, def.enable, hp.enable, speed.enable);
        }
    }

    public partial class PropAttr
    {
        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("价格")] [SerializeField]
        private Attr_Price price;

        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("稀有度")] [SerializeField]
        private Attr_Rarity rarity;

        public Attr_Price Price => price;
        public Attr_Rarity Rarity => rarity;
    }
}