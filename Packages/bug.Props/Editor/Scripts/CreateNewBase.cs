using System;
using System.Text;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    public abstract class CreateNewBase<T> where T : ScriptableObject
    {
        protected abstract string GetAssetName();

        public StringBuilder folder = new StringBuilder("Assets/Resources/Prop/SO/");

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public T propT;

        protected CreateNewBase()
        {
            propT = ScriptableObject.CreateInstance<T>();
        }

        [Button("创建", ButtonHeight = 30)]
        protected void CreateNewData()
        {
            AssetDatabase.CreateAsset(propT, $"{folder}/{GetAssetName()}.asset");
            AssetDatabase.SaveAssets();
        }
    }


    public class CreateNewPropData : CreateNewBase<PropDataSO>
    {
        public CreateNewPropData()
        {
            folder = folder.Append("MainProp");
        }

        protected override string GetAssetName() => propT.Name;
    }

    public class CreateNewPropAttrData : CreateNewBase<PropAttrSO>
    {
        public CreateNewPropAttrData()
        {
            folder = folder.Append("Attrs");
        }

        protected override string GetAssetName() => propT.attrName;
    }

    public class CreateNewPropSkillData : CreateNewBase<PropSkillSO>
    {
        public CreateNewPropSkillData()
        {
            folder = folder.Append("Skills");
        }

        protected override string GetAssetName() => propT.skillName;
    }

    public class CreateNewPropSkillAttrData : CreateNewBase<PropSkillAttrSO>
    {
        public CreateNewPropSkillAttrData()
        {
            folder = folder.Append("SkillAttrs");
        }

        protected override string GetAssetName() => propT.skillAttrName;
    }
}