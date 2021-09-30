using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "QuestManager", menuName = "Bug/Quests/QuestManager")]
public class QuestManagerSO : ScriptableObject
{
    [LabelText("任务线")] public List<QuestlineSO> questlines;
    [LabelText("背包/清单")] public InventorySO inventory;
    [LabelText("任务完成物品")] public ItemSO winningItem;
    [LabelText("任务失败物品")] public ItemSO losingItem;

    [Header("监听")] [LabelText("继续步骤事件")] public VoidEventChannelSO continueWithStepEvent;
    [LabelText("结束对话事件")] public IntEventChannelSO endDialogueEvent;
    [LabelText("成功选择事件")] public VoidEventChannelSO makeWinningChoiceEvent;
    [LabelText("失败选择事件")] public VoidEventChannelSO makeLosingChoiceEvent;

    [Header("广播")] [LabelText("播放完成任务对话事件")]
    public VoidEventChannelSO playCompletionDialogueEvent;

    [LabelText("播放失败任务对话事件")] public VoidEventChannelSO playIncompleteDialogueEvent;
    [LabelText("给物品事件")] public ItemEventChannelSO giveItemEvent;
    [LabelText("奖励物品事件")] public ItemStackEventChannelSO rewardItemEvent;

    private QuestSO currentQuest;
    private int currentQuestIndex;
    private QuestlineSO currentQuestline;
    private int currentQuestlineIndex;
    private StepSO currentStep;
    private int currentStepIndex;

    [Button("Clear Current Quest")]
    void TestClearCurrentQuest()
    {
        currentQuest = null;
        currentQuestline = null;
        currentStep = null;

        currentQuestIndex = currentQuestlineIndex = currentStepIndex = 0;
    }

    public void OnDisable()
    {
        continueWithStepEvent.OnEventRaised -= CheckStepValidity;
        endDialogueEvent.OnEventRaised -= EndDialogue;
        makeWinningChoiceEvent.OnEventRaised -= MakeWinningChoice;
        makeLosingChoiceEvent.OnEventRaised -= MakeLosingChoice;
    }

    private void OnEnable()
    {
        StartGame();
    }

    public void StartGame()
    {
        continueWithStepEvent.OnEventRaised += CheckStepValidity;
        endDialogueEvent.OnEventRaised += EndDialogue;
        makeWinningChoiceEvent.OnEventRaised += MakeWinningChoice;
        makeLosingChoiceEvent.OnEventRaised += MakeLosingChoice;
        StartQuestline();
    }

    private void StartQuestline()
    {
        if (questlines == null) return;
        if (!questlines.Exists(o => !o.isDone)) return;
        {
            currentQuestlineIndex = questlines.FindIndex(o => !o.isDone);

            if (currentQuestlineIndex >= 0)
                currentQuestline = questlines.Find(o => !o.isDone);
        }
    }

    private bool HasStep(Object actorToCheckWith)
    {
        if (currentStep == null) return false;
        return currentStep._actor == actorToCheckWith;
    }

    private bool CheckQuestlineForQuestWithActor(Object actorToCheckWith)
    {
        // 检查当前是否有任务
        if (currentQuest != null) return false;
        if (currentQuestline != null)
            return currentQuestline.quests.Exists(o =>
                !o.IsDone && o.Steps != null && o.Steps[0]._actor == actorToCheckWith);

        return false;
    }

    // 与角色互动
    public DialogueDataSO InteractWithCharacter(ActorSO actor, bool isCheckValidity, bool isValid)
    {
        if (currentQuest == null)
            if (CheckQuestlineForQuestWithActor(actor))
                StartQuest(actor);

        if (!HasStep(actor)) return null;
        if (isCheckValidity) return isValid ? currentStep._completeDialogue : currentStep._incompleteDialogue;

        return currentStep._dialogueBeforeStep;
    }

    // 当与角色互动时，我们会询问任务经理是否有一个以特定角色的步骤开始的任务
    private void StartQuest(Object actorToCheckWith)
    {
        // 检查当前是否有任务
        if (currentQuest != null) return;

        if (currentQuestline == null) return;
        // 寻找任务序号
        currentQuestIndex = currentQuestline.quests.FindIndex(o =>
            !o.IsDone && o.Steps != null && o.Steps[0]._actor == actorToCheckWith);

        if (currentQuestline.quests.Count <= currentQuestIndex || currentQuestIndex < 0) return;
        {
            currentQuest = currentQuestline.quests[currentQuestIndex];

            // 开始步骤
            currentStepIndex = 0;
            currentStepIndex = currentQuest.Steps.FindIndex(o => o._isDone == false);
            if (currentStepIndex >= 0)
                StartStep();
        }
    }

    private void MakeWinningChoice()
    {
        currentStep._item = winningItem;
        // currentStep._endStepEvent = 
        CheckStepValidity();
    }

    private void MakeLosingChoice()
    {
        currentStep._item = losingItem;
        // currentStep._endStepEvent = 
        CheckStepValidity();
    }

    private void StartStep()
    {
        if (currentQuest.Steps != null)
            if (currentQuest.Steps.Count > currentStepIndex)
                currentStep = currentQuest.Steps[currentStepIndex];
    }

    private void CheckStepValidity()
    {
        if (currentStep == null) return;

        switch (currentStep._type)
        {
            case StepType.CheckItem:
                if (inventory.Contains(currentStep._item))
                    // 触发成功对话
                    playCompletionDialogueEvent.RaiseEvent();
                else
                    // 触发失败对话
                    playIncompleteDialogueEvent.RaiseEvent();

                break;

            case StepType.GiveItem:
                if (inventory.Contains(currentStep._item))
                {
                    giveItemEvent.RaiseEvent(currentStep._item);
                    playCompletionDialogueEvent.RaiseEvent();
                }
                else
                {
                    // 触发失败对话
                    playIncompleteDialogueEvent.RaiseEvent();
                }

                break;

            case StepType.Dialogue:
                // 对话已经播放
                if (currentStep._completeDialogue != null)
                    playCompletionDialogueEvent.RaiseEvent();
                else
                    EndStep();

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void EndDialogue(int dialogueType)
    {
        // 根据结束的对话，做一些事情
        switch ((DialogueType) dialogueType)
        {
            case DialogueType.CompletionDialogue:
                if (currentStep._hasReward && currentStep._rewardItem != null)
                {
                    var itemStack = new ItemStack(currentStep._rewardItem, currentStep._rewardItemCount);
                    rewardItemEvent.RaiseEvent(itemStack);
                }

                EndStep();
                break;
            case DialogueType.StartDialogue:
                CheckStepValidity();
                break;
            case DialogueType.IncompletionDialogue:
                break;
            case DialogueType.DefaultDialogue:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dialogueType), dialogueType, null);
        }
    }

    private void EndStep()
    {
        currentStep = null;
        if (currentQuest == null) return;
        if (currentQuest.Steps.Count <= currentStepIndex) return;

        currentQuest.Steps[currentStepIndex].FinishStep();
        if (currentQuest.Steps.Count > currentStepIndex + 1)
        {
            currentStepIndex++;
            StartStep();
        }
        else
        {
            EndQuest();
        }
    }

    private void EndQuest()
    {
        if (currentQuest != null) currentQuest.FinishQuest();

        currentQuest = null;
        currentQuestIndex = -1;
        if (currentQuestline == null) return;

        if (!currentQuestline.quests.Exists(o => !o.IsDone)) EndQuestline();
    }

    private void EndQuestline()
    {
        if (questlines == null) return;

        if (currentQuestline != null) currentQuestline.FinishQuestline();

        if (questlines.Exists(o => o.isDone)) StartQuestline();
    }

    public List<string> GetFinishedQuestlineItemsGUIds()
    {
        var finishedItemsGUIds = new List<string>();

        foreach (var questline in questlines)
        {
            if (questline.isDone) finishedItemsGUIds.Add(questline.Guid);

            foreach (var quest in questline.quests)
            {
                if (quest.IsDone) finishedItemsGUIds.Add(quest.Guid);

                finishedItemsGUIds.AddRange(from step in quest.Steps where step._isDone select step.Guid);
            }
        }

        return finishedItemsGUIds;
    }

    public void SetFinishedQuestlineItemsFromSave(List<string> finishedItemsGUIds)
    {
        foreach (var questline in questlines)
        {
            questline.isDone = finishedItemsGUIds.Exists(o => o == questline.Guid);


            foreach (var quest in questline.quests)
            {
                quest.IsDone = finishedItemsGUIds.Exists(o => o == quest.Guid);


                foreach (var step in quest.Steps) step._isDone = finishedItemsGUIds.Exists(o => o == step.Guid);
            }
        }

        //Start Questline with the new data 
        StartQuestline();
    }

    public void ResetQuestlines()
    {
        foreach (var questline in questlines)
        {
            questline.isDone = false;


            foreach (var quest in questline.quests)
            {
                quest.IsDone = false;


                foreach (var step in quest.Steps) step._isDone = false;
            }
        }

        currentQuest = null;
        currentQuestline = null;
        currentStep = null;
        currentQuestIndex = 0;
        currentQuestlineIndex = 0;
        currentStepIndex = 0;
        // 使用新数据启动 Questline
        StartQuestline();
    }

    public bool IsNewGame()
    {
        var isNew = false;
        isNew = !questlines.Exists(o => o.quests.Exists(j => j.Steps.Exists(k => k._isDone)));
        return isNew;
    }
}