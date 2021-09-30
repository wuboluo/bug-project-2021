using System.Linq;
using Bug.Project21.Props;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.Skills
{
    public class SkillSO : ScriptableObject
    {
        [HideInInspector] public string[] asset;

        // ------------------------------------------------------------ 

        [LabelText("名称")]
        [VerticalGroup("Basic")]
        [BoxGroup("Basic/通用")]
        [VerticalGroup("Basic/通用/Right")]
        [SerializeField]
        [InlineButton(nameof(Rename), "↺")]
        private new string name;
        
        
        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Right")]
        [LabelText("技能类型")] [SerializeField] 
        private PropSkillType type;
        
        [BoxGroup("Basic/通用")] [VerticalGroup("Basic/通用/Right")]
        [LabelText("技能图标")] [SerializeField]  [PreviewField(40)]
        private Texture2D icon;

        [BoxGroup("Basic/描述")] 
        [HideLabel] [SerializeField] [TextArea(3, 10)]
        private string skillDescribe;

        [BoxGroup("Basic/属性")] 
        [LabelText("作用于")] [SerializeField]
        private SkillAttrActingOn target;

        [BoxGroup("Basic/属性")] 
        [LabelText("AOE")] [SerializeField]
        private bool isAOE;

        [BoxGroup("Basic/属性")] 
        [LabelText("基础伤害")] [SerializeField]
        private float value;

        [BoxGroup("Basic/属性")] 
        [LabelText("影响比例")] [SerializeField] [Range(0, 5)]
        private float percent;

        [BoxGroup("Basic/音特效")] 
        [LabelText("特效")] [SerializeField]
        private ParticleSystem effect;

        [BoxGroup("Basic/音特效")] 
        [LabelText("音效")] [SerializeField]
        private AudioSource audio;

        // ------------------------------------------------------------ 

        public string Name
        {
            get => name;
            set => name = value;
        }

        public PropSkillType Type => type;
        public Texture2D Icon => icon;
        public string SkillDescribe => skillDescribe;
        public SkillAttrActingOn Target => target;
        public bool IsAOE => isAOE;
        public float Percent => percent;
        public float Value => value;
        public ParticleSystem Effect => effect;
        public AudioSource Audio => audio;

        private void Rename()
        {
            AssetDatabase.RenameAsset(AssetDatabase.GUIDToAssetPath(asset.First()), Name);
        }
    }
}