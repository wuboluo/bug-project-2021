using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.PropsEditor
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

        [ToggleGroup("Enabled")] [LabelText("CD")] [LabelWidth(100)] [SerializeField]
        private float cd;

        [ToggleGroup("Enabled")] [LabelText("执行耗时")] [LabelWidth(100)] [SerializeField]
        private float time;

        [ToggleGroup("Enabled")] [LabelText("影响范围（%）")] [LabelWidth(100)] [SerializeField] [Range(0, 1)]
        private float value;

        [ToggleGroup("Enabled")] [LabelText("影响速度")] [LabelWidth(100)] [SerializeField]
        private AnimationCurve valueCurve;

        // ------------------------------------------------------------ 

        [ToggleGroup("Enabled", "$Label")] public bool Enabled = true;

        public string Name
        {
            get => name;
            set => name = value;
        }

        public SkillAttrActingOn Target => target;
        public bool IsAOE => isAOE;
        public float Cd => cd;
        public float Time => time;
        public float Value => value;
        public AnimationCurve ValueCurve => valueCurve;

        public string Label => name;
        // ------------------------------------------------------------ 
    }
}