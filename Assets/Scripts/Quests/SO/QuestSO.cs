using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Quest", menuName = "Bug/Quests/Quest")]
public class QuestSO : SerializableScriptableObject
{
    public int idQuest;
    public List<StepSO> steps = new List<StepSO>();

    public bool isDone;

    public VoidEventChannelSO endQuestEvent;

    /// <summary>
    ///     引发 结束任务 事件，标记此任务为已完成
    /// </summary>
    public void FinishQuest()
    {
        endQuestEvent?.RaiseEvent();
        isDone = true;
    }

    public void SetQuestId(int id)
    {
        idQuest = id;
    }
}