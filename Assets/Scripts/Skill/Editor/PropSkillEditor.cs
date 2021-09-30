using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.Props
{
    public class PropSkillEditor : OdinMenuEditorWindow
    {
        [MenuItem("碧油鸡/工具/技能编辑器")]
        private static void Open()
        {
            var window = GetWindow<PropSkillEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            window.titleContent = new GUIContent("技能编辑器");
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 25f;
            tree.Config.DrawSearchToolbar = true;

            tree.AddAllAssetsAtPath("", "Assets/ScriptableObjects/PropSkill", typeof(SkillSO), true)
                .ForEach(AddDragHandles);

            tree.EnumerateTree().Where(r => r.Value as SkillSO).ForEach(AddDragHandles);
            // tree.EnumerateTree().AddIcons<PropSkillDataSO>(r => r.Icon);

            return tree;
        }

        private static void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += r => DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }

        protected override void OnBeginDrawEditors()
        {
            var selected = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;

            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                if (selected != null) GUILayout.Label(selected.Name);

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("创建")))
                    SOCreator.ShowDialog<SkillSO>("Assets/ScriptableObjects/PropSkill",
                        obj =>
                        {
                            obj.Name = obj.name;
                            TrySelectMenuItemWithObject(obj);

                            obj.asset = AssetDatabase.FindAssets(obj.Name, new[] {"Assets/ScriptableObjects/PropSkill"});
                        });

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("删除")))
                    if (selected != null)
                    {
                        var asset = AssetDatabase.FindAssets(selected.Name, new[] {"Assets/ScriptableObjects/PropSkill"});
                        AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(asset.First()));
                    }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}