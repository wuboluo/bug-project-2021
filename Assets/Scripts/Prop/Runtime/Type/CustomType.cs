using UnityEngine;

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