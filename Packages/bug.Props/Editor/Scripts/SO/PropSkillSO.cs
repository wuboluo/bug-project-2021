using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewPropSkill", menuName = "Props/Skill/PropSkill")]
    public class PropSkillSO : PropSOBase
    {
        [LabelText("名称")] [SerializeField]
        private new string name;
        
        [Space(15)]
        
        // 主动 or 被动
        [LabelText("技能类型")] [SerializeField] private PropSkillTypeSO propSkillTypeSo;

        // 技能描述
        [LabelText("技能描述")][SerializeField] private string skillDescribe;

        // 技能属性
        [LabelText("技能属性")] [SerializeField] private PropSkillAttrSO propSkillAttr;

        // 特效
        [LabelText("特效组")] [SerializeField] private ParticleSystem effect;

        // 音效
        [LabelText("音效组")][SerializeField] private AudioSource audio;
        
        
        public override string Name
        {
            get => name;
            set => name = value;
        }

        public PropSkillTypeSO PropSkillTypeSo => propSkillTypeSo;
        public string SkillDescribe => skillDescribe;
        public PropSkillAttrSO PropSkillAttr => propSkillAttr;
        public ParticleSystem Effect => effect;
        public AudioSource Audio => audio;
    }
}