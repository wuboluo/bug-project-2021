using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

namespace Bug.Project21.Backpack
{
    public enum BackpackType { On = 0, Off = 1 }
    public class BackpackManager : MonoBehaviour
    {
        /// <summary>
        /// 背包
        /// </summary>
        public GameObject BackpackPrefab = null;
        /// <summary>
        /// 背包开关按钮
        /// </summary>
        public Button OnButtonBackpackOpen = null;
        /// <summary>
        /// 背包关闭按钮
        /// </summary>
        public Button OnButtonBackpackClose = null;
        /// <summary>
        /// 背包状态
        /// </summary>
        public BackpackType _backpackType = BackpackType.Off;

        void Start()
        {
            _backpackType = BackpackPrefab.transform.localScale != Vector3.one ? BackpackType.Off : BackpackType.On;
            OnButtonBackpackOpen.onClick.AddListener(() =>
            {
                switch (_backpackType)
                {
                    case BackpackType.On:
                        BackpackPrefab.transform.localScale = Vector3.zero;
                        _backpackType = BackpackType.Off;
                        break;
                    case BackpackType.Off:
                        BackpackPrefab.transform.localScale = Vector3.one;
                        _backpackType = BackpackType.On;
                        break;
                }
            });
            OnButtonBackpackClose.onClick.AddListener(() =>
            {
                switch (_backpackType)
                {
                    case BackpackType.On:
                        BackpackPrefab.transform.localScale = Vector3.zero;
                        _backpackType = BackpackType.Off;
                        break;
                    case BackpackType.Off:
                        BackpackPrefab.transform.localScale = Vector3.one;
                        _backpackType = BackpackType.On;
                        break;
                }
            });

            //string file_path = UnityEditor.EditorUtility.OpenFilePanel("Load Images of Directory", UnityEngine.Application.dataPath, "xlsx");

        }


        void Update()
        {
        
        }
    }
}
