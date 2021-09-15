using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


public class PropsEditorWindow : EditorWindow
{
    [MenuItem("Bug/Tools/PropsEditor")]
    public static void ShowExample()
    {
        PropsEditorWindow wnd = GetWindow<PropsEditorWindow>();
        wnd.titleContent = new GUIContent("PropsEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.bug.unity.project21.props/Editor/Window/PropsEditorWindow.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);
    }
}