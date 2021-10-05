using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<ActorSO> actorsList;

    [Header("Listening")] 
    [SerializeField] private DialogueDataChannelSO _startDialogue;
    [SerializeField] private DialogueChoiceChannelSO _makeDialogueChoiceEvent;

    [Header("Broadcasting")]
    [SerializeField] private DialogueLineChannelSO _openUIDialogueEvent;
    [SerializeField] private IntEventChannelSO _endDialogueWithTypeEvent;
    [SerializeField] private VoidEventChannelSO _continueWithStep;
    [SerializeField] private VoidEventChannelSO _playIncompleteDialogue;
    [SerializeField] private VoidEventChannelSO _makeWinningChoice;
    [SerializeField] private VoidEventChannelSO _makeLosingChoice;

    // ------- temp -------
    public GameObject uiChoices;
    public TextMeshProUGUI uiActor;
    public TextMeshProUGUI uiSentence;


    private DialogueDataSO _currentDialogue;
    public DialogueInputControl dialogueInputControl;

    
    private int _counterDialogue;
    private int _counterLine;
    private bool _reachedEndOfDialogue => _counterDialogue >= _currentDialogue.lines.Count;
    private bool _reachedEndOfLine => _counterLine >= _currentDialogue.lines[_counterDialogue].textList.Count;


    private void Awake()
    {
        dialogueInputControl = new DialogueInputControl();
    }

    private void Start()
    {
        _startDialogue.OnEventRaised += DisplayDialogueData;
        dialogueInputControl.Dialogue.Next.performed += _ => OnAdvance();
    }

    private void OnEnable()
    {
        dialogueInputControl.Enable();
    }

    private void OnDisable()
    {
        dialogueInputControl.Disable();
    }

    /// <summary>
    ///    显示对话内容，显示在 UI上
    /// </summary>
    public void DisplayDialogueData(DialogueDataSO dialogueDataSO)
    {
        _counterDialogue = 0;
        _counterLine = 0;
        _currentDialogue = dialogueDataSO;

        if (_currentDialogue.lines != null)
        {
            var currentActor = actorsList.Find(o => o._actorId == _currentDialogue.lines[_counterDialogue].actorID);

            DisplayDialogueLine(_currentDialogue.lines[_counterDialogue].textList[_counterLine], currentActor);
        }
        else
        {
            Debug.LogError("Check Dialogue");
        }
    }

    /// <summary>
    ///     UI上显示一行对话 【Talker : Content】
    /// </summary>
    public void DisplayDialogueLine(string dialogueLine, ActorSO actor)
    {
        _openUIDialogueEvent.RaiseEvent(dialogueLine, actor);


        uiActor.text = actor == null ? "Me" : actor.name;
        uiSentence.text = dialogueLine;
        if (_reachedEndOfLine) uiActor.text = uiSentence.text = string.Empty;
    }

    /// <summary>
    ///     下一步，切换行 or 段
    /// </summary>
    private void OnAdvance()
    {
        _counterLine++;
        var _choices = _currentDialogue.lines[_counterDialogue].choices;

        if (!_reachedEndOfLine)
        {
            var currentActor = actorsList.Find(o => o._actorId == _currentDialogue.lines[_counterDialogue].actorID);
            DisplayDialogueLine(_currentDialogue.lines[_counterDialogue].textList[_counterLine], currentActor);
        }
        else if (_choices != null && _choices.Count > 0)
        {
            DisplayChoices(_currentDialogue.lines[_counterDialogue].choices);
        }
        else
        {
            _counterDialogue++;
            if (!_reachedEndOfDialogue)
            {
                _counterLine = 0;

                var currentActor =
                    actorsList.Find(o => o._actorId == _currentDialogue.lines[_counterDialogue].actorID);
                DisplayDialogueLine(_currentDialogue.lines[_counterDialogue].textList[_counterLine], currentActor);
            }
            else
            {
                DialogueEndedAndCloseDialogueUI();
            }
        }
    }

    // todo: 传入 choices
    /// <summary>
    ///     显示选项
    /// </summary>
    private void DisplayChoices(List<DialogueDataSO.Choice> choices)
    {
        _makeDialogueChoiceEvent.OnEventRaised += MakeDialogueChoice;
        uiChoices.SetActive(true);
    }

    /// <summary>
    ///     做出对话选择
    /// </summary>
    private void MakeDialogueChoice(DialogueDataSO.Choice choice)
    {
        _makeDialogueChoiceEvent.OnEventRaised -= MakeDialogueChoice;

        switch (choice.actionType)
        {
            case ChoiceActionType.ContinueWithStep:
                if (_continueWithStep != null)
                    _continueWithStep.RaiseEvent();
                if (choice.nextDialogue != null)
                    DisplayDialogueData(choice.nextDialogue);
                break;

            case ChoiceActionType.WinningChoice:
                if (_makeWinningChoice != null)
                    _makeWinningChoice.RaiseEvent();
                break;

            case ChoiceActionType.LosingChoice:
                if (_makeLosingChoice != null)
                    _makeLosingChoice.RaiseEvent();
                break;

            case ChoiceActionType.DoNothing:
                if (choice.nextDialogue != null)
                    DisplayDialogueData(choice.nextDialogue);
                else
                    DialogueEndedAndCloseDialogueUI();
                break;

            case ChoiceActionType.IncompleteStep:
                if (_playIncompleteDialogue != null)
                    _playIncompleteDialogue.RaiseEvent();
                if (choice.nextDialogue != null)
                    DisplayDialogueData(choice.nextDialogue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    /// <summary>
    ///     对话结束和关闭对话 UI
    /// </summary>
    private void DialogueEndedAndCloseDialogueUI()
    {
        // 执行 当前对话完成要做的事
        _currentDialogue.FinishDialogue();

        // 根据对话类型，引发 当前对话结束事件
        _endDialogueWithTypeEvent?.RaiseEvent((int) _currentDialogue.dialogueType);
    }
}