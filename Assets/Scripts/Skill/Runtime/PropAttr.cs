using System;
using System.Collections.Generic;
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
    }

    public partial class PropAttr
    {
        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("价格")] [SerializeField]
        private Attr_Price price;

        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("稀有度")] [SerializeField]
        private Attr_Rarity rarity;
    }

    public partial class PropAttr
    {
        [HideInInspector] public bool isWeapon;

        /// <summary>
        ///     判断道具属性的值是否都被设置过
        /// </summary>
        /// <returns>true：全部被设置</returns>
        public bool InitValues(PropTag tag)
        {
            switch (tag)
            {
                case PropTag.weapon:
                {
                    var values = new[] {atk.Value, def.Value, hp.Value, speed.Value};
                    return !values.All(_ => _.Equals(default));
                }
                case PropTag.stuff:
                {
                    var values = new[] {price.Value, rarity.Value};
                    return !values.All(_ => _.Equals(default));
                }
                default:
                    return false;
            }
        }

        /// <summary>
        ///     设置道具属性的数值（范围随机）
        /// </summary>
        public void SetValues(PropTag tag)
        {
            switch (tag)
            {
                case PropTag.weapon:
                    atk.InitValue();
                    def.InitValue();
                    hp.InitValue();
                    speed.InitValue();
                    break;
                case PropTag.stuff:
                    price.InitValue();
                    rarity.InitValue();
                    break;
            }
        }

        /// <summary>
        ///     获取道具属性的值
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, int> GetValues(PropTag tag)
        {
            return tag switch
            {
                PropTag.weapon => new Dictionary<string, int>
                {
                    {"atk", atk.Value}, {"def", def.Value}, {"hp", hp.Value}, {"speed", speed.Value}
                },
                PropTag.stuff => new Dictionary<string, int>
                {
                    {"price", price.Value}, {"rarity", rarity.Value}
                },
                _ => null
            };
        }


        /// <summary>
        ///     获取属性是否可使用
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, bool> GetAbles(PropTag tag)
        {
            return tag switch
            {
                PropTag.weapon => new Dictionary<string, bool>
                {
                    {"atk", atk.enable}, {"def", def.enable}, {"hp", hp.enable}, {"speed", speed.enable}
                },
                PropTag.stuff => new Dictionary<string, bool>
                {
                    {"price", price.enable}, {"rarity", rarity.enable}
                },
                _ => null
            };
        }
    }
}