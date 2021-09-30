using System.Linq;
using Bug.Project21.Skills;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.Props
{
    public partial class PropSO : ScriptableObject
    {
        [HideInInspector] public string[] asset;

        public Texture2D Icon
        {
            get => mainIcon;
            set => mainIcon = value;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Describe => describe;
        public int Price => price;
        public int ID => id;
        public PropTag Tag => tag;
        public PropAttr Attrs => attrs;

        private void SwitchPropTag()
        {
            attrs.isWeapon = Tag switch
            {
                PropTag.equip => true,
                PropTag.stuff => false,
                _ => attrs.isWeapon
            };
        }

        private void Rename()
        {
            AssetDatabase.RenameAsset(AssetDatabase.GUIDToAssetPath(asset.First()), Name);
        }
    }

    public partial class PropSO
    {
        [VerticalGroup("Basic")] [HorizontalGroup("Basic/通用/Basic", 100, LabelWidth = 50)]
        [HideLabel] [SerializeField] [PreviewField(100)]
        private Texture2D mainIcon;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")]
        [LabelText("名称")] [SerializeField] [InlineButton(nameof(Rename), "↺")]
        private new string name;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")] 
        [LabelText("价值")] [SerializeField]
        private int price;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")] 
        [LabelText("ID")] [SerializeField]
        private int id;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")]
        [LabelText("标签")] [SerializeField] [OnValueChanged(nameof(SwitchPropTag))]
        private PropTag tag;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")] 
        [LabelText("类型")] [SerializeField]
        private EquipType equipType;

        [BoxGroup("Basic/描述")] 
        [HideLabel] [SerializeField] [TextArea(3, 5)]
        private string describe;

        [BoxGroup("Basic/技能")] [HideLabel]
        public SkillSO skill;

        [BoxGroup("Basic/属性")] [HideLabel] [SerializeField]
        private PropAttr attrs;
    }
}