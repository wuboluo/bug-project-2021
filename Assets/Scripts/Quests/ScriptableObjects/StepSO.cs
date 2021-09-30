using Sirenix.OdinInspector;
using UnityEngine;

public enum StepType
{
    Dialogue,
    GiveItem,
    CheckItem
}


[CreateAssetMenu(fileName = "new Step", menuName = "Bug/Quests/Step")]
public class StepSO : SerializableScriptableObject
{
    [LabelText("Npc")] public ActorSO _actor;
    [LabelText("起始对话")] public DialogueDataSO _dialogueBeforeStep;
    [LabelText("Yes对话")] public DialogueDataSO _completeDialogue;
    [LabelText("No对话")] public DialogueDataSO _incompleteDialogue;
    [LabelText("步骤类型")] public StepType _type;
    [LabelText("目标物品")] public ItemSO _item;
    [LabelText("是否有奖励")] public bool _hasReward;
    [LabelText("奖励物品")] public ItemSO _rewardItem;
    [LabelText("奖励数")] public int _rewardItemCount = 1;
    [LabelText("是否完成")] public bool _isDone;
    [LabelText("步骤结束事件")] public VoidEventChannelSO _endStepEvent;

    public void FinishStep()
    {
        if (_endStepEvent != null)
            _endStepEvent.RaiseEvent();
        _isDone = true;
    }
}