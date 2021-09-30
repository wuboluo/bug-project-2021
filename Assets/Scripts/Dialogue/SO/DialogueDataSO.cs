using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

#endif

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
    [SerializeField] private List<Line> _lines;
    [SerializeField] private DialogueType _dialogueType;
    [SerializeField] private VoidEventChannelSO _endOfDialogueEvent;

    public VoidEventChannelSO EndOfDialogueEvent => _endOfDialogueEvent;
    public List<Line> Lines => _lines;

    public DialogueType DialogueType
    {
        get => _dialogueType;
        set => _dialogueType = value;
    }

    public void FinishDialogue()
    {
        if (EndOfDialogueEvent != null)
            EndOfDialogueEvent.RaiseEvent();
    }

    [Serializable]
    public class Choice
    {
        [SerializeField] private DialogueDataSO _nextDialogue;
        [SerializeField] private ChoiceActionType _actionType;

        public Choice(Choice choice)
        {
            _nextDialogue = choice.NextDialogue;
            _actionType = ActionType;
        }

        public DialogueDataSO NextDialogue => _nextDialogue;
        public ChoiceActionType ActionType => _actionType;

        public void SetNextDialogue(DialogueDataSO dialogue)
        {
            _nextDialogue = dialogue;
        }
    }

    [Serializable]
    public class Line
    {
        public int actorID;
        public string actor;
        public List<string> _textList;
        public List<Choice> _choices;


        public Line()
        {
            _textList = null;
        }
    }
}