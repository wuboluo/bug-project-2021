using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewPropSkill", menuName = "Props/Skill/PropSkill")]
    public class PropSkillSO : ScriptableObject
    {
        // 主动 or 被动
        [SerializeField] private PropSkillTypeSO propSkillTypeSo;
        public PropSkillTypeSO PropSkillTypeSo => propSkillTypeSo;
        
        // 技能描述
        [SerializeField] private string skillDescribe;
        public string SkillDescribe => skillDescribe;

        // 技能属性
        [SerializeField] private PropSkillAttrSO propSkillAttr;
        public PropSkillAttrSO PropSkillAttr => propSkillAttr;
        
        // 特效
        [SerializeField] private ParticleSystem effect;
        public ParticleSystem Effect => effect;
        
        // 音效
        [SerializeField] private AudioSource audio;
        public AudioSource Audio => audio;
    }
}