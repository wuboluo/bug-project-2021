using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [Serializable]
    public class PropSkillSO
    {
        public string Name
        {
            get => name;
            set => name = value;
        }
        
        public PropSkillType Type => type;
        public string SkillDescribe => skillDescribe;
        public PropSkillAttrSO PropSkillAttr => propSkillAttr;
        public ParticleSystem Effect => effect;
        public AudioSource Audio => audio;
        
        // ------------------------------------------------------------ 
        
        [LabelText("名称")] [ToggleGroup("Enabled")] [SerializeField]
        private string name;
        
        [ToggleGroup("Enabled")] [LabelText("技能类型")] [SerializeField]
        private PropSkillType type;
        
        [ToggleGroup("Enabled")] [LabelText("技能描述")] [SerializeField]
        private string skillDescribe;
        
        [ToggleGroup("Enabled")] [LabelText("技能属性")] [SerializeField]
        private PropSkillAttrSO propSkillAttr;
        
        //todo: 特效组和音效组 或许需要配置 SO
        
        [ToggleGroup("Enabled")] [LabelText("特效组")] [SerializeField]
        private ParticleSystem effect;
        
        [ToggleGroup("Enabled")] [LabelText("音效组")] [SerializeField]
        private AudioSource audio;
        
        // ------------------------------------------------------------ 
        
        [ToggleGroup("Enabled", "$Label")] public bool Enabled = true;
        public string Label => name;
    }
}