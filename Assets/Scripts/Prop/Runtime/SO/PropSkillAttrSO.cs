using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewPropSkillAttr", menuName = "Props/Skill/PropSkillAttr")]
    public class PropSkillAttrSO : PropSOBase
    {
        [LabelText("名称")] [SerializeField]
        private new string name;

        [Space(15)]

        // 增加移动速度
        [LabelText("增加移速")] [SerializeField] private int addSpeed;
        
        
        public override string Name
        {
            get => name;
            set => name = value;
        }
        public int AddSpeed => addSpeed;
    }
}
