using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomsPass))]
public class CustomPassEditor : Editor
{
    CustomsPass mPass = null;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (mPass == null)
        {
            mPass = (CustomsPass)target;
        }
        else
        {
            if (GUILayout.Button("添加Enemy刷新点"))
            {
                mPass.OnCreateEnemyGroup();
            }
            if (GUILayout.Button("隐藏Enemy编辑点"))
            {
                mPass.OnHideEnemyGroup();
            }
            if (GUILayout.Button("显示Enemy编辑点"))
            {
                mPass.OnShowEnemyGroup();
            }


            if (GUILayout.Button("添加Boss刷新点"))
            {
                mPass.OnCreateBossGroup();
            }
            if (GUILayout.Button("隐藏Boss编辑点"))
            {
                mPass.OnHideBossGroup();
            }
            if (GUILayout.Button("显示Boss编辑点"))
            {
                mPass.OnShowBossGroup();
            }
        }
    }

}
