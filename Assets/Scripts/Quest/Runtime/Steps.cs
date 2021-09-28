using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.Quest
{
    [Serializable]
    public class ExchangeObj
    {
        [HorizontalGroup("Ex", 0.15f)] [HideLabel]
        public GameObject obj;

        [HorizontalGroup("Ex")] [HideLabel] public int count;
    }

    [Serializable]
    public class Check
    {
        [HideInInspector] public bool result;

        [TabGroup("Yes")] [LabelText("完成任务对话")]
        public List<QuestDialogue> yesDias;

        [TabGroup("No")] [LabelText("继续任务对话")] public List<QuestDialogue> noDias;
    }


    [Serializable]
    public class QuestDialogue
    {
        [HorizontalGroup("Dia", 0.15f)] [HideLabel]
        public Identity identity;

        [HorizontalGroup("Dia")] [HideLabel] public string content;
    }


    public enum Identity
    {
        NPC,
        Player
    }
}