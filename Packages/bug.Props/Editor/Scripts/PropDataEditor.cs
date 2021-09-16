using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.PropsEditor
{
    public class PropDataEditor : OdinMenuEditorWindow
    {
        private CreateNewPropData _createNewPropData;

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_createNewPropData != null)
                DestroyImmediate(_createNewPropData.propData);
        }

        [MenuItem("Bug/Tools/道具编辑器")]
        private static void OpenWindow()
        {
            GetWindow<PropDataEditor>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();

            _createNewPropData = new CreateNewPropData();
            tree.Add("空白道具模板", _createNewPropData);
            tree.AddAllAssetsAtPath("道具组", "Assets/Resources/PropTest/SO", typeof(PropDataSO));

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
                    var asset = selected.SelectedValue as PropDataSO;
                    var path = AssetDatabase.GetAssetPath(asset);
                    AssetDatabase.DeleteAsset(path);
                    AssetDatabase.SaveAssets();
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }


    public class CreateNewPropData
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public PropDataSO propData;

        public CreateNewPropData()
        {
            propData = ScriptableObject.CreateInstance<PropDataSO>();
        }

        [Button("创建", ButtonHeight = 30)]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(propData, $"Assets/Resources/PropTest/SO/{propData.Name}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}