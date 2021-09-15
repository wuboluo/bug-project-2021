using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewProp", menuName = "Props/PropData")]
    public class PropDataSO : ScriptableObject
    {
        // ID，用于确定惟一性
        [SerializeField] private int id;
        public int ID => id;

        // 标签，区分武器、材料等
        [SerializeField] private PropTagSO tag;
        public PropTagSO Tag => tag;

        // 当前道具图标
        [SerializeField] private Texture2D mainIcon;
        public Texture2D MainIcon => mainIcon;

        // 下级合成物品列表（不需要可为空）
        [SerializeField] private List<PropDataSO> lowLevelProps = new List<PropDataSO>();
        public List<PropDataSO> LowLevelProps => lowLevelProps;

        // 道具名称
        [SerializeField] private string name;
        public string Name => name;

        // 道具价值
        [SerializeField] private int price;
        public int Price => price;

        // 道具描述
        [SerializeField] private string describe;
        public string Describe => describe;

        // 属性，例如 +10 Atk
        [SerializeField] private PropAttrSO attr;
        public PropAttrSO Attr => attr;

        // 技能，包括主动、被动
        [SerializeField] private List<PropSkillSO> skills = new List<PropSkillSO>();
        public List<PropSkillSO> Skills => skills;
    }
}