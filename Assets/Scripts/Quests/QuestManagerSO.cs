using System.Collections.Generic;
using System.Linq;
using Bug.Project21.Dialogue;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Bug.Project21.Quest
{
    [CreateAssetMenu(fileName = "QuestManager", menuName = "Bug/Quests/QuestManager")]
    public class QuestManagerSO : ScriptableObject
    {
        public List<QuestlineSO> questlines;
        public InventorySO inventory;

        [Header("Listening")] [LabelText("步骤结束之后")]
        public VoidEventChannelSO _continueWithStepEvent;

        [LabelText("对话结束时")] public IntEventChannelSO _endDialogueEvent;
        [LabelText("做出肯定选项")] public VoidEventChannelSO _makeWinningChoiceEvent;
        [LabelText("做出否定选项")] public VoidEventChannelSO _makeLosingChoiceEvent;

        [Header("Broadcasting")] [LabelText("播放任务完成对话")]
        public VoidEventChannelSO _playCompletionDialogueEvent;

        [LabelText("播放任务失败对话")] public VoidEventChannelSO _playIncompleteDialogueEvent;
        [LabelText("给予物品")] public ItemEventChannelSO _giveItemEvent;
        [LabelText("奖励物品")] public ItemStackEventChannelSO _rewardItemEvent;

        private QuestSO currentQuest;
        private int currentQuestIndex;
        private QuestlineSO currentQuestline;
        private int currentQuestlineIndex;
        private StepSO currentStep;
        private int currentStepIndex;

        private void OnEnable()
        {
            _continueWithStepEvent.OnEventRaised += StepToDoAccordingToType;
            _endDialogueEvent.OnEventRaised += EndDialogue;
            _makeWinningChoiceEvent.OnEventRaised += MakeWinChoice;
            _makeLosingChoiceEvent.OnEventRaised += MakeLostChoice;
            SetQuestline();
        }

        public void OnDisable()
        {
            _continueWithStepEvent.OnEventRaised -= StepToDoAccordingToType;
            _endDialogueEvent.OnEventRaised -= EndDialogue;
            _makeWinningChoiceEvent.OnEventRaised -= MakeWinChoice;
            _makeLosingChoiceEvent.OnEventRaised -= MakeLostChoice;
        }


        [Space(30)] [LabelText("测试：任务线")] [InlineButton(nameof(TestClearCurrentQuest), "重置任务线")]
        public QuestlineSO testQL;

        private void TestClearCurrentQuest()
        {
            currentQuest = null;
            currentQuestline = null;
            currentStep = null;

            currentQuestIndex = currentQuestlineIndex = currentStepIndex = 0;

            testQL.quests.First()?.steps.ForEach(_ => _.isDone = false);
            testQL.isDone = testQL.quests.First().isDone = false;
        }


        /// <summary>
        ///     从任务线库中寻找未完成的任务线，设置 当前任务线序号 和 当前任务线
        /// </summary>
        private void SetQuestline()
        {
            if (questlines == null) return;
            if (!questlines.Exists(o => !o.isDone)) return;
            {
                currentQuestlineIndex = questlines.FindIndex(o => !o.isDone);

                if (currentQuestlineIndex >= 0)
                    currentQuestline = questlines.Find(o => !o.isDone);
            }
        }

        /// <summary>
        ///     执行 当前任务线结束之后要做的事。检查任务线库中是否还有未完成的任务线，有则设置新的任务线
        /// </summary>
        private void EndQuestline()
        {
            if (questlines == null) return;

            if (currentQuestline != null) currentQuestline.FinishQuestline();

            if (questlines.Exists(o => o.isDone)) SetQuestline();
        }

        /// <summary>
        ///     设置 当前任务序号：当前任务线中查找（未完成、步骤不为空、步骤一的 Actor为传入的 Actor）的任务。
        ///     若序号有效，根据序号设置 当前任务。
        ///     查找此任务中第一个未完成的步骤，并设置当前步骤序号、设置步骤
        /// </summary>
        private void StartQuest(Object actorToCheckWith)
        {
            if (currentQuest != null) return;
            if (currentQuestline == null) return;

            currentQuestIndex = currentQuestline.quests.FindIndex(o =>
                !o.isDone && o.steps != null && o.steps[0].actor == actorToCheckWith);

            if (currentQuestline.quests.Count > currentQuestIndex && currentQuestIndex >= 0)
            {
                currentQuest = currentQuestline.quests[currentQuestIndex];

                currentStepIndex = currentQuest.steps.FindIndex(o => o.isDone == false);
                if (currentStepIndex >= 0)
                    SetStep();
            }
        }

        /// <summary>
        ///     执行 当前任务结束之后要做的事。检查当前任务线所有任务完成状态，若全部完成，结束任务线。
        /// </summary>
        private void EndQuest()
        {
            if (currentQuest != null) currentQuest.FinishQuest();

            currentQuest = null;
            currentQuestIndex = -1;
            if (currentQuestline == null) return;

            if (!currentQuestline.quests.Exists(o => !o.isDone)) EndQuestline();
        }

        /// <summary>
        ///     若当前步骤不为当前任务的最后一个步骤，根据当前步骤序号设置 当前步骤
        /// </summary>
        private void SetStep()
        {
            if (currentQuest.steps == null) return;
            if (currentQuest.steps.Count > currentStepIndex)
                currentStep = currentQuest.steps[currentStepIndex];
        }

        /// <summary>
        ///     步骤结束时，检查是否继续下一个步骤 或 结束任务
        /// </summary>
        private void EndStep()
        {
            currentStep = null;

            // 若当前任务不为空，并且当前步骤不为最后一个步骤
            if (currentQuest == null) return;
            if (currentQuest.steps.Count <= currentStepIndex) return;

            // 执行当前步骤完成时需要做的事
            currentQuest.steps[currentStepIndex].FinishStep();

            // 若不为最后一个步骤，则继续下一个步骤。否则结束任务
            if (currentQuest.steps.Count > currentStepIndex + 1)
            {
                currentStepIndex++;
                SetStep();
            }
            else
            {
                EndQuest();
            }
        }

        /// <summary>
        ///     检查当前任务的当前步骤的 Actor是否为传入的 Actor，
        ///     若不是，影响：没有后续的相关对话
        /// </summary>
        private bool HasStep(Object actorToCheckWith)
        {
            if (currentStep == null) return false;
            return currentStep.actor == actorToCheckWith;
        }

        /// <summary>
        ///     检查当前任务线是否包含符合要求的任务。
        ///     1、未完成
        ///     2、步骤不为空
        ///     3、步骤一的 Actor为传入的 NPC
        /// </summary>
        private bool CheckQuestlineForQuestWithActor(Object actorToCheckWith)
        {
            if (currentQuest != null) return false;
            return currentQuestline != null
                   && currentQuestline.quests.Exists(o =>
                       !o.isDone && o.steps != null && o.steps[0].actor == actorToCheckWith);
        }

        /// <summary>
        ///     和 Actor(NPC) 不同阶段的对话。【hasSthToDo】对话中是否需要做什么事 【SthIsDone】若需要做什么，是否做完
        /// </summary>
        public DialogueDataSO DifferentDialoguesWithActor(ActorSO actor, bool hasSthToDo, bool SthIsDone)
        {
            // 如果当前任务为空，则根据传入的 actor分配任务
            if (currentQuest == null)
                if (CheckQuestlineForQuestWithActor(actor))
                    StartQuest(actor);

            if (!HasStep(actor)) return null;
            if (hasSthToDo) return SthIsDone ? currentStep.completeDialogue : currentStep.incompleteDialogue;

            return currentStep.startDialogue;
        }

        /// <summary>
        ///     根据步骤类型设置当前步骤需要做什么事
        /// </summary>
        private void StepToDoAccordingToType()
        {
            if (currentStep == null) return;

            switch (currentStep.type)
            {
                case StepType.CheckItem:
                    if (inventory.Contains(currentStep.item))
                        _playCompletionDialogueEvent.RaiseEvent();
                    else
                        _playIncompleteDialogueEvent.RaiseEvent();
                    break;

                case StepType.GiveItem:
                    if (inventory.Contains(currentStep.item))
                    {
                        _giveItemEvent.RaiseEvent(currentStep.item);
                        _playCompletionDialogueEvent.RaiseEvent();
                    }
                    else
                    {
                        _playIncompleteDialogueEvent.RaiseEvent();
                    }

                    break;

                case StepType.Dialogue:
                    if (currentStep.completeDialogue != null)
                        _playCompletionDialogueEvent.RaiseEvent();
                    else
                        EndStep();
                    break;
            }
        }

        /// <summary>
        ///     对话完成时，被监听。
        ///     在 StartDialogue之后，根据对话类型检查对话完成需要做什么。
        ///     在 CompletionDialogue之后，检查是否存在需要奖励的物品，若存在，引发 奖励物品 事件
        /// </summary>
        private void EndDialogue(int dialogueType)
        {
            switch ((DialogueType) dialogueType)
            {
                case DialogueType.StartDialogue:
                    StepToDoAccordingToType();
                    break;

                case DialogueType.CompletionDialogue:
                    if (currentStep.hasReward && currentStep.rewardItem != null)
                    {
                        var itemStack = new ItemStack(currentStep.rewardItem, currentStep.rewardItemCount);
                        _rewardItemEvent.RaiseEvent(itemStack);
                    }

                    EndStep();
                    break;
            }
        }


        private void MakeWinChoice()
        {
            StepToDoAccordingToType();
        }

        private void MakeLostChoice()
        {
            StepToDoAccordingToType();
        }
    }
}