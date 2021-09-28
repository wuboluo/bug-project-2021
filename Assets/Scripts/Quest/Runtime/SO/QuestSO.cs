using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Bug.Project21.Quest
{
    public class QuestSO : ScriptableObject
    {
        [SerializeField] [LabelText("名称")] [InlineButton(nameof(Rename), "↺")]
        private new string name;

        [HideInInspector] public string[] asset;

        [LabelText("推送任务对象")] public QuestPusher pusher;
        public bool isDone;

        [BoxGroup("对话")] [LabelText("初次见面")] public List<QuestDialogue> diasOnFirstMeet;

        [BoxGroup("对话")] [LabelText("检查任务")] public List<QuestDialogue> diasOnCheck;

        [BoxGroup("交换物品")] [LabelText("目标")] public List<ExchangeObj> targetObjs;

        [BoxGroup("交换物品")] [LabelText("奖励")] public List<ExchangeObj> rewards;

        [LabelText("检查结果")] public Check check;

        public string Name
        {
            get => name;
            set => name = value;
        }

        private void Rename()
        {
            AssetDatabase.RenameAsset(AssetDatabase.GUIDToAssetPath(asset.First()), Name);
        }
    }
}