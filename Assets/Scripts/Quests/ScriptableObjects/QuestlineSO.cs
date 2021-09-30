using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Questline", menuName = "Bug/Quests/Questline")]
public class QuestlineSO : SerializableScriptableObject
{
    public int idQuestLine;
    public List<QuestSO> quests = new List<QuestSO>();

    public bool isDone;

    private readonly VoidEventChannelSO endQuestlineEvent = default;

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