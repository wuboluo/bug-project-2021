using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewPropAttr", menuName = "Props/PropAttr")]
    public class PropAttrSO : ScriptableObject
    {
        public string attrName;
        
        // 攻击力
        [SerializeField] private int atk;
        public int Atk => atk;
        
        // 防御力
        [SerializeField] private int def;
        public int Def => def;
        
        // 生命值
        [SerializeField] private int hp;
        public int Hp => hp;
        
        // 移动速度
        [SerializeField] private int speed;
        public int Speed => speed;
    }
}
