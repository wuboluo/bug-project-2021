using System.Linq;
using Bug.Project21.PropsEditor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class PropSkillDataSO : ScriptableObject
{
    [HideInInspector] public string[] asset;

    // ------------------------------------------------------------ 

    [LabelText("名称")]
    [HorizontalGroup("Basic", 0.4f, MarginLeft = 0, LabelWidth = 70)]
    [VerticalGroup("Basic/Left")]
    [BoxGroup("Basic/Left/通用")]
    [VerticalGroup("Basic/Left/通用/Right")]
    [SerializeField]
    [InlineButton(nameof(Rename), "↺")]
    private new string name;

    [LabelText("技能类型")] [SerializeField] [BoxGroup("Basic/Left/通用")] [VerticalGroup("Basic/Left/通用/Right")]
    private PropSkillType type;

    [FoldoutGroup("Basic/Left/描述")] [HideLabel] [SerializeField] [TextArea(3, 10)]
    private string skillDescribe;

    [FoldoutGroup("Basic/Left/音特效")] [LabelText("特效")] [SerializeField]
    private ParticleSystem effect;

    [FoldoutGroup("Basic/Left/音特效")] [LabelText("音效")] [SerializeField]
    private AudioSource audio;

    [VerticalGroup("Basic/Right")] [LabelText("技能")] [SerializeField]
    private PropSkillAttr[] skillAttrs;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public PropSkillType Type => type;
    public string SkillDescribe => skillDescribe;
    public ParticleSystem Effect => effect;
    public AudioSource Audio => audio;

    /// <summary>
    /// 技能列表
    /// </summary>
    public PropSkillAttr[] SkillAttrs => skillAttrs;

    // ------------------------------------------------------------ 

    private void Rename()
    {
        AssetDatabase.RenameAsset(AssetDatabase.GUIDToAssetPath(asset.First()), Name);
    }
}