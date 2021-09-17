using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewPropAttr", menuName = "Props/PropAttr")]
    public class PropAttrSO : PropSOBase
    {
        [LabelText("名称")] [SerializeField]
        private new string name;

        [Space(15)]

        // 攻击力
        [LabelText("攻击")][SerializeField] private int atk;
        
        // 防御力
        [LabelText("防御")] [SerializeField] private int def;
        
        // 生命值
        [LabelText("生命值")] [SerializeField] private int hp;
        
        // 移动速度
        [LabelText("移动速度")]  [SerializeField] private int speed;
        
        
        
        
        public override string Name
        {
            get => name;
            set => name = value;
        }
        public int Atk => atk;
        public int Def => def;
        public int Hp => hp;
        public int Speed => speed;
    }
}
