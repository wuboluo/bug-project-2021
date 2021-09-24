using UnityEngine;

namespace Bug.Project21.Props
{
    public enum PropTag
    {
        [InspectorName("材料")] stuff,
        [InspectorName("武器")] weapon
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
}