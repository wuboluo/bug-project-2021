using System.Linq;
using Bug.Project21.Skills;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bug.Project21.Props
{
    public partial class PropSO : ScriptableObject
    {
        // Record the guid of the current asset, used in the editor script.
        [HideInInspector] public string[] asset;

        public Texture2D Icon
        {
            get => mainIcon;
            set => mainIcon = value;
        }

        public string PropName
        {
            get => propName;
            set => propName = value;
        }

        [field: BoxGroup("Basic/技能")]
        [field: HideLabel]
        public SkillSO Skill { get; set; }

        public string Describe => describe;
        public int Price => price;
        public int ID => id;
        public GameObject Prefab => prefab;
        public PropTag Tag => tag;
        public EquipType EquipType => equipType;
        public PropAttr Attrs => attrs;

        // On propTag was Changed, show different attrs in editor window.
        private void SwitchPropTag()
        {
            attrs.isWeapon = Tag switch
            {
                PropTag.equip => true,
                PropTag.stuff => false,
                _ => attrs.isWeapon
            };
        }

        // Asset rename, not the name of the object, used in the editor window.
        private void Rename()
        {
            AssetDatabase.RenameAsset(AssetDatabase.GUIDToAssetPath(asset.First()), PropName);
        }
    }

    public partial class PropSO
    {
        [VerticalGroup("Basic")]
        [HorizontalGroup("Basic/通用/Basic", 70, LabelWidth = 50)]
        [HideLabel]
        [SerializeField]
        [PreviewField(70)]
        private Texture2D mainIcon;

        [FormerlySerializedAs("name")]
        [BoxGroup("Basic/通用")]
        [VerticalGroup("Basic/通用/Basic/Right")]
        [LabelText("名称")]
        [SerializeField]
        [InlineButton(nameof(Rename), "↺")]
        private string propName;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")] [LabelText("价值")] [SerializeField]
        private int price;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")] [LabelText("ID")] [SerializeField]
        private int id;

        [BoxGroup("Basic/通用")]
        [VerticalGroup("Basic/通用/Basic/Right")]
        [LabelText("标签")]
        [SerializeField]
        [OnValueChanged(nameof(SwitchPropTag))]
        private PropTag tag;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")] [LabelText("类型")] [SerializeField]
        private EquipType equipType;

        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Basic/Right")] [LabelText("预制体")] [SerializeField]
        private GameObject prefab;

        [BoxGroup("Basic/描述")] [HideLabel] [SerializeField] [TextArea(3, 5)]
        private string describe;

        [BoxGroup("Basic/属性")] [HideLabel] [SerializeField]
        private PropAttr attrs;
    }
}