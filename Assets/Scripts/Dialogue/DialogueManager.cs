using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<ActorSO> actorsList;

    [Header("监听 Add.Remove")] 
    [LabelText("开始对话")] [Tooltip("Raise在 NPC<StepController>")] [SerializeField]
    private DialogueDataChannelSO _startDialogue;
    [LabelText("做选择")] [Tooltip("Raise在 Button.onClick-UnityEvent（Inspector）")] [SerializeField]
    private DialogueChoiceChannelSO _makeDialogueChoiceEvent;

    [Header("广播 Raise")] 
    [LabelText("对话结束")] [Tooltip("监听于 NPC<StepController>")] [SerializeField]
    private IntEventChannelSO _endDialogueWithTypeEvent;
    [LabelText("选择肯定选项")] [Tooltip("监听于 QuestManagerSO")] [SerializeField]
    private VoidEventChannelSO _makeWinningChoice;
    [LabelText("选择否定选项")] [Tooltip("监听于 QuestManagerSO")] [SerializeField]
    private VoidEventChannelSO _makeLosingChoice;
    [LabelText("播放任务失败对话")] [Tooltip("")] [SerializeField]
    private VoidEventChannelSO _playIncompleteDialogue;

    [SerializeField] private VoidEventChannelSO _continueWithStep;


    [Header("UI")] 
    public DialogueLineChannelSO _openUIDialogueEvent;
    public VoidEventChannelSO _closeUIDialogueEvent;
    public VoidEventChannelSO _showChoiceEvent;
    public VoidEventChannelSO _hideChoiceEvent;


    private DialogueDataSO _currentDialogue;

    private int _counterDialogue;
    private int _counterLine;
    public DialogueInputControl dialogueInputControl;
    private bool _reachedEndOfDialogue => _counterDialogue >= _currentDialogue.lines.Count;
    private bool _reachedEndOfLine => _counterLine >= _currentDialogue.lines[_counterDialogue].textList.Count;


    private void Awake()
    {
        dialogueInputControl = new DialogueInputControl();
    }

    private void Start()
    {
        _startDialogue.OnEventRaised += DisplayDialogueData;
        dialogueInputControl.Dialogue.Next.performed += _ => Continue();
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
    ///     显示对话内容，显示在 UI上
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
    }


    /// <summary>
    ///     继续。下一行，若最后一行则继续下一段
    /// </summary>
    private void Continue()
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
            DisplayChoices();
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

    /// <summary>
    ///     显示选项
    /// </summary>
    private void DisplayChoices()
    {
        _makeDialogueChoiceEvent.OnEventRaised += MakeDialogueChoice;
        _showChoiceEvent.RaiseEvent();
    }

    /// <summary>
    ///     选择后，选项衔接的对话SO
    /// </summary>
    private void MakeDialogueChoice(DialogueDataSO.Choice choice)
    {
        _makeDialogueChoiceEvent.OnEventRaised -= MakeDialogueChoice;
        _hideChoiceEvent?.RaiseEvent();

        switch (choice.actionType)
        {
            case ChoiceActionType.ContinueWithStep:
                _continueWithStep?.RaiseEvent();
                if (choice.nextDialogue != null)
                    DisplayDialogueData(choice.nextDialogue);
                break;

            case ChoiceActionType.WinningChoice:
                _makeWinningChoice?.RaiseEvent();
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
                _playIncompleteDialogue?.RaiseEvent();
                if (choice.nextDialogue != null)
                    DisplayDialogueData(choice.nextDialogue);
                break;
        }
    }

    /// <summary>
    ///     对话结束和关闭对话 UI
    /// </summary>
    private void DialogueEndedAndCloseDialogueUI()
    {
        Debug.Log("对话结束，关闭对话UI窗口");

        _currentDialogue.FinishDialogue();

        // 根据对话类型，引发 当前对话结束事件
        _endDialogueWithTypeEvent?.RaiseEvent((int) _currentDialogue.dialogueType);

        _closeUIDialogueEvent.RaiseEvent();
    }
}