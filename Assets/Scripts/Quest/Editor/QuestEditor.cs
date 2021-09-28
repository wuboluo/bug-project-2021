using System.Linq;
using Bug.Project21.Props;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.Quest
{
    public class QuestEditor : OdinMenuEditorWindow
    {
        [MenuItem("碧油鸡/工具/任务编辑器")]
        private static void Open()
        {
            var window = GetWindow<QuestEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            window.titleContent = new GUIContent("任务编辑器");
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 25f;
            tree.Config.DrawSearchToolbar = true;

            tree.AddAllAssetsAtPath("", "Assets/ScriptableObjects/Quest", typeof(QuestSO), true)
                .ForEach(AddDragHandles);

            tree.EnumerateTree().Where(r => r.Value as QuestSO).ForEach(AddDragHandles);

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
                    SOCreator.ShowDialog<QuestSO>("Assets/ScriptableObjects/Quest",
                        obj =>
                        {
                            obj.Name = obj.name;
                            TrySelectMenuItemWithObject(obj);

                            obj.asset = AssetDatabase.FindAssets(obj.Name,
                                new[] {"Assets/ScriptableObjects/Quest"});
                        });

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("删除")))
                    if (selected != null)
                    {
                        var asset = AssetDatabase.FindAssets(selected.Name,
                            new[] {"Assets/ScriptableObjects/Questline/SO"});
                        AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(asset.First()));
                    }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}