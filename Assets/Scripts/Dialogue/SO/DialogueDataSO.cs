using System;
using System.Collections.Generic;
using UnityEngine;




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

[CreateAssetMenu(fileName = "new Dialogue", menuName = "Bug/Dialogue/Dialogue Data")]
public class DialogueDataSO : ScriptableObject
{
    public List<Line> lines;
    public DialogueType dialogueType;
    public VoidEventChannelSO endOfDialogueEvent;

    /// <summary>
    ///     引发 对话结束事件
    /// </summary>
    public void FinishDialogue()
    {
        endOfDialogueEvent?.RaiseEvent();
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