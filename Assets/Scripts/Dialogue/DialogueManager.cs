using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<ActorSO> _actorsList;

    [Header("监听")] [LabelText("开始对话")] [SerializeField]
    private DialogueDataChannelSO _startDialogue;

    [LabelText("进行对话选择")] [SerializeField] private DialogueChoiceChannelSO _makeDialogueChoiceEvent = default;

    [Header("广播")] [LabelText("打开UI对话")] [SerializeField]
    private DialogueLineChannelSO _openUIDialogueEvent;

    [LabelText("结束对话类型")] [SerializeField] private IntEventChannelSO _endDialogueWithTypeEvent;
    [LabelText("继续步骤")] [SerializeField] private VoidEventChannelSO _continueWithStep;

    [LabelText("播放未完成任务对话")] [SerializeField]
    private VoidEventChannelSO _playIncompleteDialogue;

    [LabelText("做出完成任务选择")] [SerializeField]
    private VoidEventChannelSO _makeWinningChoice;

    [LabelText("做出未完成任务选择")] [SerializeField]
    private VoidEventChannelSO _makeLosingChoice;

    private int _counterDialogue;
    private int _counterLine;
    private bool _reachedEndOfDialogue => _counterDialogue >= _currentDialogue.Lines.Count;
    private bool _reachedEndOfLine => _counterLine >= _currentDialogue.Lines[_counterDialogue]._textList.Count;

    private DialogueDataSO _currentDialogue;

    public DialogueInputControl dialogueInputControl;

    // ------- temp -------
    public GameObject uiChoices;
    public TextMeshProUGUI uiActor;
    public TextMeshProUGUI uiSentence;


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

    // 在 UI中显示对话
    public void DisplayDialogueData(DialogueDataSO dialogueDataSO)
    {
        _counterDialogue = 0;
        _counterLine = 0;
        _currentDialogue = dialogueDataSO;

        if (_currentDialogue.Lines != null)
        {
            var currentActor =
                _actorsList.Find(o => o._actorId == _currentDialogue.Lines[_counterDialogue].actorID);

            DisplayDialogueLine(_currentDialogue.Lines[_counterDialogue]._textList[_counterLine], currentActor);
        }
        else
        {
            Debug.LogError("Check Dialogue");
        }
    }

    /// <summary>
    /// 显示一行对话（内容，讲话的角色）
    /// </summary>
    /// <param name="dialogueLine"></param>
    /// <param name="actor"></param>
    public void DisplayDialogueLine(string dialogueLine, ActorSO actor)
    {
        _openUIDialogueEvent.RaiseEvent(dialogueLine, actor);


        uiActor.text = actor == null ? "Me" : actor.name;
        uiSentence.text = dialogueLine;
        if (_reachedEndOfLine) uiActor.text = uiSentence.text = string.Empty;
    }

    /// <summary>
    /// 下一步，切换行 or 段
    /// </summary>
    private void OnAdvance()
    {
        _counterLine++;
        var _choices = _currentDialogue.Lines[_counterDialogue]._choices;

        if (!_reachedEndOfLine)
        {
            var currentActor = _actorsList.Find(o => o._actorId == _currentDialogue.Lines[_counterDialogue].actorID);
            DisplayDialogueLine(_currentDialogue.Lines[_counterDialogue]._textList[_counterLine], currentActor);
        }
        else if (_choices != null && _choices.Count > 0)
        {
            DisplayChoices(_currentDialogue.Lines[_counterDialogue]._choices);
        }
        else
        {
            _counterDialogue++;
            if (!_reachedEndOfDialogue)
            {
                _counterLine = 0;

                var currentActor =
                    _actorsList.Find(o => o._actorId == _currentDialogue.Lines[_counterDialogue].actorID);
                DisplayDialogueLine(_currentDialogue.Lines[_counterDialogue]._textList[_counterLine], currentActor);
            }
            else
            {
                DialogueEndedAndCloseDialogueUI();
            }
        }
    }

    // todo: 传入 choices
    /// <summary>
    /// 显示选项
    /// </summary>
    /// <param name="choices"></param>
    private void DisplayChoices(List<DialogueDataSO.Choice> choices)
    {
        _makeDialogueChoiceEvent.OnEventRaised += MakeDialogueChoice;
        uiChoices.SetActive(true);
    }

    /// <summary>
    /// 做出对话选择
    /// </summary>
    /// <param name="choice"></param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private void MakeDialogueChoice(DialogueDataSO.Choice choice)
    {
        _makeDialogueChoiceEvent.OnEventRaised -= MakeDialogueChoice;

        switch (choice.ActionType)
        {
            case ChoiceActionType.ContinueWithStep:
                if (_continueWithStep != null)
                    _continueWithStep.RaiseEvent();
                if (choice.NextDialogue != null)
                    DisplayDialogueData(choice.NextDialogue);
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
                if (choice.NextDialogue != null)
                    DisplayDialogueData(choice.NextDialogue);
                else
                    DialogueEndedAndCloseDialogueUI();
                break;

            case ChoiceActionType.IncompleteStep:
                if (_playIncompleteDialogue != null)
                    _playIncompleteDialogue.RaiseEvent();
                if (choice.NextDialogue != null)
                    DisplayDialogueData(choice.NextDialogue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    /// <summary>
    /// 对话结束和关闭对话 UI
    /// </summary>
    private void DialogueEndedAndCloseDialogueUI()
    {
        // 如果有的话，引发对话结束的特殊事件
        _currentDialogue.FinishDialogue();

        // 引发结束对话事件
        if (_endDialogueWithTypeEvent != null)
            _endDialogueWithTypeEvent.RaiseEvent((int) _currentDialogue.DialogueType);
    }
}