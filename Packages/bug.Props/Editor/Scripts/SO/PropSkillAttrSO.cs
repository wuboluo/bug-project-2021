using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewPropSkillAttr", menuName = "Props/Skill/PropSkillAttr")]
    public class PropSkillAttrSO : ScriptableObject
    {
        // 增加移动速度
        [SerializeField] private int addSpeed;
        public int AddSpeed => addSpeed;
        
    }
}
