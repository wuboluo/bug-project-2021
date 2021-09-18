using UnityEditor;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;

namespace Bug.Project21.PropsEditor
{
    public class PropEditor : OdinMenuEditorWindow
    {
        [MenuItem("Bug/Tools/PropEditor")]
        private static void Open()
        {
            var window = GetWindow<PropEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 500);
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree(true);
            tree.DefaultMenuStyle.IconSize = 25f;
            tree.Config.DrawSearchToolbar = true;

            tree.AddAllAssetsAtPath("", "Assets/Resources/Prop/SO", typeof(PropSOBase), true)
                .ForEach(AddDragHandles);
            
            tree.EnumerateTree().Where(r => r.Value as PropSOBase).ForEach(AddDragHandles);
            tree.EnumerateTree().AddIcons<PropSOBase>(r => r.Icon);

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
                if (selected != null)
                {
                    GUILayout.Label(selected.Name);
                }

                if (SirenixEditorGUI.ToolbarButton(new GUIContent("Create")))
                {
                    SOCreator.ShowDialog<PropSOBase>("Assets/Resources/Prop/SO",
                        obj =>
                        {
                            obj.Name = obj.name;
                            TrySelectMenuItemWithObject(obj);
                        });
                }
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }
    }
}