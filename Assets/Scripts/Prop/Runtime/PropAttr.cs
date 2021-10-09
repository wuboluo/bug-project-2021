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
        private PropAttr_Atk atk;

        [ShowIf("isWeapon")] [Toggle("enable")] [LabelText("防御")] [SerializeField]
        private PropAttr_Def def;

        [ShowIf("isWeapon")] [Toggle("enable")] [LabelText("生命值")] [SerializeField]
        private PropAttr_Hp hp;

        [ShowIf("isWeapon")] [Toggle("enable")] [LabelText("移动速度")] [SerializeField]
        private PropAttr_Speed speed;
    }

    public partial class PropAttr
    {
        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("价格")] [SerializeField]
        private PropAttr_Price price;

        [HideIf("isWeapon")] [Toggle("enable")] [LabelText("稀有度")] [SerializeField]
        private PropAttr_Rarity rarity;
    }

    public partial class PropAttr
    {
        [HideInInspector] public bool isWeapon;

        /// <summary>
        ///     Determine whether the value of the 'PropAttr' has been set.
        /// </summary>
        public bool InitValues(PropTag tag)
        {
            switch (tag)
            {
                case PropTag.equip:
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
        ///     Set the value of the 'PropAttr' (random range).
        /// </summary>
        public void SetValues(PropTag tag)
        {
            switch (tag)
            {
                case PropTag.equip:
                    atk.InitValue();
                    def.InitValue();
                    hp.InitValue();
                    speed.InitValue();
                    break;
                case PropTag.stuff:
                    price.InitValue();
                    rarity.InitValue();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tag), tag, null);
            }
        }

        /// <summary>
        ///     Get the value of the prop value.
        /// </summary>
        public Dictionary<string, int> GetValues(PropTag tag)
        {
            return tag switch
            {
                PropTag.equip => new Dictionary<string, int>
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
        ///     Get whether the attr is available.
        /// </summary>
        public Dictionary<string, bool> GetAbles(PropTag tag)
        {
            return tag switch
            {
                PropTag.equip => new Dictionary<string, bool>
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