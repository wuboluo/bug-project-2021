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
    public ActorSO actor;
    public StepType type;

    [Header("Dialogue")] public DialogueDataSO startDialogue;

    public DialogueDataSO completeDialogue;
    public DialogueDataSO incompleteDialogue;

    [Header("Want")] public ItemSO item;

    [Header("Reward")] public bool hasReward;

    public ItemSO rewardItem;
    public int rewardItemCount = 1;

    [Header("Result")] public bool isDone;

    public VoidEventChannelSO endStepEvent;

    /// <summary>
    ///     引发 结束步骤事件，标记此步骤为已完成
    /// </summary>
    public void FinishStep()
    {
        endStepEvent?.RaiseEvent();
        isDone = true;
    }
}