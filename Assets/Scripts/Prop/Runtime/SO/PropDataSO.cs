using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.Props
{
    public partial class PropDataSO : ScriptableObject
    {
        [HideInInspector] public string[] asset;

        public Texture Icon
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

        public GameObject Prefab => prefab;

        public PropAttr Attrs => attrs;

        private void SwitchPropTag()
        {
            attrs.isWeapon = Tag switch
            {
                PropTag.weapon => true,
                PropTag.stuff => false,
                _ => attrs.isWeapon
            };
        }

        private void Rename()
        {
            AssetDatabase.RenameAsset(AssetDatabase.GUIDToAssetPath(asset.First()), Name);
        }
    }

    public partial class PropDataSO
    {
        [HideLabel]
        [PreviewField(80)]
        [VerticalGroup("Basic/Left")]
        [HorizontalGroup("Basic/Left/通用/Basic", 80, LabelWidth = 70)]
        [SerializeField]
        private Texture mainIcon;

        [BoxGroup("Basic/Left/通用")]
        [VerticalGroup("Basic/Left/通用/Basic/Right")]
        [SerializeField]
        [InlineButton(nameof(Rename), "↺")]
        [LabelText("名称")]
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

        [HorizontalGroup("Basic", 0.4f, MarginLeft = 0, LabelWidth = 70)]
        [VerticalGroup("Basic/Right")]
        [FoldoutGroup("Basic/Right/实例")]
        [HideLabel]
        [SerializeField]
        private GameObject prefab;
        
        [FoldoutGroup("Basic/Left/描述")] [HideLabel] [SerializeField] [TextArea(3, 10)]
        private string describe;

        [HorizontalGroup("Basic", 0.4f, MarginLeft = 0, LabelWidth = 70)]
        [VerticalGroup("Basic/Right")]
        [FoldoutGroup("Basic/Right/属性")]
        [HideLabel]
        [SerializeField]
        private PropAttr attrs;

        [BoxGroup("Basic/Left/技能")] [HideLabel]
        public PropSkillDataSO skillDataSO;
    }
}