using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.Props
{
    public class PropEditor : OdinMenuEditorWindow
    {
        [MenuItem("碧油鸡/工具/道具编辑器")]
        private static void Open()
        {
            var window = GetWindow<PropEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
            window.titleContent = new GUIContent("道具编辑器");
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 25f;
            tree.Config.DrawSearchToolbar = true;

            tree.AddAllAssetsAtPath("", "Assets/ScriptableObjects/Prop", typeof(PropSO), true)
                .ForEach(AddDragHandles);

            tree.EnumerateTree().Where(r => r.Value as PropSO).ForEach(AddDragHandles);
            tree.EnumerateTree().AddIcons<PropSO>(r => r.Icon);

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
                    SOCreator.ShowDialog<PropSO>("Assets/ScriptableObjects/Prop",
                        obj =>
                        {
                            obj.PropName = obj.name;
                            TrySelectMenuItemWithObject(obj);

                            obj.asset = AssetDatabase.FindAssets(obj.PropName, new[] {"Assets/ScriptableObjects/Prop"});
                        });

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("删除")))
                    if (selected != null)
                    {
                        var asset = AssetDatabase.FindAssets(selected.Name, new[] {"Assets/ScriptableObjects/Prop"});
                        AssetDatabase.DeleteAsset(AssetDatabase.GUIDToAssetPath(asset.First()));
                    }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}