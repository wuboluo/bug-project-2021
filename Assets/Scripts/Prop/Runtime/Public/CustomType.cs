using UnityEngine;

namespace Bug.Project21.Props
{
    public enum PropTag
    {
        [InspectorName("材料")] stuff,
        [InspectorName("装备")] equip
    }

    public enum PropSkillType
    {
        [InspectorName("主动技能")] active,
        [InspectorName("被动技能")] passive
    }

    public enum SkillAttrActingOn
    {
        [InspectorName("健康")] hp,
        [InspectorName("攻击")] atk,
        [InspectorName("防御")] def,
        [InspectorName("移动")] speed
    }

    public enum EquipType
    {
        [InspectorName("武器")] weapon,
        [InspectorName("铠甲")] armor,
        [InspectorName("鞋子")] shoes
    }
}