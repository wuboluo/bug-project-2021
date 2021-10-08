using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Project21.Dialogue
{
    [CreateAssetMenu(fileName = "new Dialogue", menuName = "Bug/Dialogue/Dialogue Data")]
    public class DialogueDataSO : ScriptableObject
    {
        public List<Line> lines;
        public DialogueType dialogueType;
        public VoidEventChannelSO endOfDialogueEvent;

        // 任务需求在 UI上的显示
        public StringEventChannelSO questTargetOnUIEvent;
        [SerializeField] private string displayOnUI = string.Empty;
        public string DisplayOnUI => displayOnUI;

        /// <summary>
        ///     引发 对话结束事件
        /// </summary>
        public void FinishDialogue()
        {
            endOfDialogueEvent?.RaiseEvent();

            // 结束后 广播任务目标需求事件
            questTargetOnUIEvent?.RaiseEvent(displayOnUI);
        }

        [Serializable]
        public class Choice
        {
            public DialogueDataSO nextDialogue;
            public ChoiceActionType actionType;

            public Choice(Choice choice)
            {
                nextDialogue = choice.nextDialogue;
            }

            public void SetNextDialogue(DialogueDataSO dialogue)
            {
                nextDialogue = dialogue;
            }
        }

        [Serializable]
        public class Line
        {
            public int actorID;
            public string actor;
            public List<string> textList;
            public List<Choice> choices;

            public Line()
            {
                textList = null;
            }
        }
    }

    public enum DialogueType
    {
        StartDialogue,
        CompletionDialogue,
        IncompletionDialogue,
        DefaultDialogue
    }

    public enum ChoiceActionType
    {
        DoNothing,
        ContinueWithStep,
        WinningChoice,
        LosingChoice,
        IncompleteStep
    }
}