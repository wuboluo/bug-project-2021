using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    [CreateAssetMenu(fileName = "NewProp", menuName = "Props/PropData")]
    public partial class PropDataSO : PropSOBase
    {
        public override Texture Icon
        {
            get => mainIcon;
            set => mainIcon = value;
        }

        public override string Name
        {
            get => name;
            set => name = value;
        }

        public string Describe => describe;
        public int Price => price;
        public int ID => id;
        public PropTag Tag => tag;
        public List<PropDataSO> LowLevelProps => lowLevelProps;
        public PropAttrSO Attrs => attrs;
        public PropSkillSO[] Skills => skills;


        private void SwitchPropTag()
        {
            attrs.isWeapon = Tag switch
            {
                PropTag.weapon => true,
                PropTag.stuff => false,
                _ => attrs.isWeapon
            };
        }
    }

    public partial class PropDataSO
    {
        // ------------------------------------------------------------ Left

        [HideLabel]
        [PreviewField(80)]
        [VerticalGroup("Basic/Left")]
        [HorizontalGroup("Basic/Left/通用/Basic", 80, LabelWidth = 60)]
        [SerializeField]
        private Texture mainIcon;

        [BoxGroup("Basic/Left/通用")] [VerticalGroup("Basic/Left/通用/Basic/Right")] [LabelText("名称")] [SerializeField]
        private new string name;

        [BoxGroup("Basic/Left/通用")] [VerticalGroup("Basic/Left/通用/Basic/Right")] [LabelText("价值")] [SerializeField]
        private int price;

        [BoxGroup("Basic/Left/通用")] [VerticalGroup("Basic/Left/通用/Basic/Right")] [LabelText("ID")] [SerializeField]
        private int id;

        [BoxGroup("Basic/Left/通用")]
        [VerticalGroup("Basic/Left/通用/Basic/Right")]
        [LabelText("标签")]
        [SerializeField]
        [OnValueChanged(nameof(SwitchPropTag))]
        private PropTag tag;

        [FoldoutGroup("Basic/Left/属性")] [HideLabel] [SerializeField]
        private PropAttrSO attrs;

        // ------------------------------------------------------------ Right

        [HorizontalGroup("Basic", 0.5f, MarginLeft = 5, LabelWidth = 120)]
        [BoxGroup("Basic/Right/描述")]
        [SerializeField]
        [HideLabel]
        [TextArea(3, 10)]
        private string describe;

        [VerticalGroup("Basic/Right")] [LabelText("技能")] [SerializeField]
        private PropSkillSO[] skills;

        [Space(5)] [LabelWidth(50)] [LabelText("合成路线")] [SerializeField]
        private List<PropDataSO> lowLevelProps = new List<PropDataSO>();
    }
}