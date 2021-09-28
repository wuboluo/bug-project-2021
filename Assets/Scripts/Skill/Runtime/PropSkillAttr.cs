using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.Props
{
    [Serializable]
    public class PropSkillAttr
    {
        [LabelText("名称")] [ToggleGroup("Enabled")] [LabelWidth(100)] [SerializeField]
        private string name;

        [ToggleGroup("Enabled")] [LabelText("作用于")] [LabelWidth(100)] [SerializeField]
        private SkillAttrActingOn target;

        [ToggleGroup("Enabled")] [LabelText("AOE")] [LabelWidth(100)] [SerializeField]
        private bool isAOE;

        [ToggleGroup("Enabled")] [LabelText("基础伤害")] [LabelWidth(100)] [SerializeField]
        private float value;

        [ToggleGroup("Enabled")] [LabelText("影响比例")] [LabelWidth(100)] [SerializeField] [Range(0, 1)]
        private float percent;

        // ------------------------------------------------------------ 

        [ToggleGroup("Enabled", "$Label")] public bool Enabled = true;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public SkillAttrActingOn Target => target;

        public bool IsAOE => isAOE;
        public float Percent => percent;
        public float Value => value;
        public string Label => name;
    }
}