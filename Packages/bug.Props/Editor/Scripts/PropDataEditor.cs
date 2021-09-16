using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    public class PropDataEditor : OdinMenuEditorWindow
    {
        private CreateNewPropData _createNewPropData;
        private CreateNewPropAttrData _createNewPropAttrData;
        private CreateNewPropSkillData _createNewPropSkillData;
        private CreateNewPropSkillAttrData _createNewPropSkillAttrData;

        private string root;

        [MenuItem("Bug/Tools/道具编辑器")]
        private static void OpenWindow()
        {
            GetWindow<PropDataEditor>().Show();
        }

        protected override void OnEnable()
        {
            _createNewPropData = new CreateNewPropData();
            _createNewPropAttrData = new CreateNewPropAttrData();
            _createNewPropSkillData = new CreateNewPropSkillData();
            _createNewPropSkillAttrData = new CreateNewPropSkillAttrData();

            root = _createNewPropData.folder.ToString();
            root = root.Substring(0, root.LastIndexOf('/') + 1);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_createNewPropData != null)
                DestroyImmediate(_createNewPropData.propT);

            if (_createNewPropAttrData != null)
                DestroyImmediate(_createNewPropAttrData.propT);

            if (_createNewPropSkillData != null)
                DestroyImmediate(_createNewPropSkillData.propT);

            if (_createNewPropSkillAttrData != null)
                DestroyImmediate(_createNewPropSkillAttrData.propT);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            
            tree.Add("空白道具模板", _createNewPropData);
            tree.AddAllAssetsAtPath("道具组", $"{root}MainProp", typeof(PropDataSO));

            tree.Add("空白属性模板", _createNewPropAttrData);
            tree.AddAllAssetsAtPath("属性组", $"{root}Attrs", typeof(PropAttrSO));

            tree.Add("空白技能模板", _createNewPropSkillData);
            tree.AddAllAssetsAtPath("技能组", $"{root}Skills", typeof(PropSkillSO));

            tree.Add("空白技能属性模板", _createNewPropSkillAttrData);
            tree.AddAllAssetsAtPath("技能属性组", $"{root}SkillAttrs", typeof(PropSkillAttrSO));

            Debug.Log("11111111111111");
            
            return tree;
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = MenuTree.Selection;

            SirenixEditorGUI.BeginHorizontalToolbar();
            {
                GUILayout.FlexibleSpace();

                if (SirenixEditorGUI.ToolbarButton("删除当前配置"))
                {
                    switch (selected.SelectedValue)
                    {
                        case PropDataSO asset:
                            DeleteAsset(asset);
                            break;
                        case PropAttrSO assetA:
                            DeleteAsset(assetA);
                            break;
                        case PropSkillSO assetS:
                            DeleteAsset(assetS);
                            break;
                        case PropSkillAttrSO assetSA:
                            DeleteAsset(assetSA);
                            break;
                    }
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private static void DeleteAsset(Object so)
        {
            var path = AssetDatabase.GetAssetPath(so);
            AssetDatabase.DeleteAsset(path);
            AssetDatabase.SaveAssets();
        }
    }
}