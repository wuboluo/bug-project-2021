using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Questline", menuName = "Bug/Quests/Questline")]
public class QuestlineSO : SerializableScriptableObject
{
    public int idQuestLine;
    public List<QuestSO> quests = new List<QuestSO>();

    public bool isDone;

    private readonly VoidEventChannelSO endQuestlineEvent = default;

    /// <summary>
    ///     引发 结束任务线 事件，标记此任务线为已完成
    /// </summary>
    public void FinishQuestline()
    {
        endQuestlineEvent?.RaiseEvent();
        isDone = true;
    }

    public void SetQuestlineId(int id)
    {
        idQuestLine = id;
    }
}