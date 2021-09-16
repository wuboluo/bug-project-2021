using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewProp", menuName = "Props/PropData")]
    public partial class PropDataSO : ScriptableObject
    {
        // ---------------------------------------- Display on UI
        [HorizontalGroup("PropData", 60)] [PreviewField(60)] [HideLabel] [SerializeField]
        private Texture2D mainIcon;

        [VerticalGroup("PropData/Stats")] [LabelWidth(50)] [LabelText("名称")] [SerializeField]
        private new string name;

        [VerticalGroup("PropData/Stats")] [LabelWidth(50)] [LabelText("描述")] [SerializeField]
        private string describe;

        [VerticalGroup("PropData/Stats")] [LabelWidth(50)] [LabelText("价值")] [SerializeField]
        private int price;


        // ---------------------------------------- For program
        [Space(10)]
        [LabelWidth(50)] [LabelText("ID")] [SerializeField]
        private int id;

        [LabelWidth(50)] [LabelText("标签")] [SerializeField]
        private PropTagSO tag;


        // ---------------------------------------- Attribute & Skill
        [Space(10)]
        [LabelWidth(50)] [LabelText("属性")] [SerializeField]
        private List<PropAttrSO> attrs = new List<PropAttrSO>();

        [LabelWidth(50)] [LabelText("技能")] [SerializeField]
        private List<PropSkillSO> skills = new List<PropSkillSO>();


        // ---------------------------------------- Synthetic Route
        [Space(10)]
        [LabelWidth(50)] [LabelText("合成路线")] [SerializeField]
        private List<PropDataSO> lowLevelProps = new List<PropDataSO>();
        
    }

    public partial class PropDataSO
    {
        public Texture2D MainIcon => mainIcon;
        public string Name => name;
        public string Describe => describe;
        public int Price => price;

        public int ID => id;
        public PropTagSO Tag => tag;
        public List<PropDataSO> LowLevelProps => lowLevelProps;
        public List<PropAttrSO> Attr => attrs;
        public List<PropSkillSO> Skills => skills;
    }
}