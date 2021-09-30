using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Quest", menuName = "Bug/Quests/Quest")]
public class QuestSO : SerializableScriptableObject
{
    [SerializeField] private int _idQuest;

    [SerializeField] private List<StepSO> _steps = new List<StepSO>();

    [SerializeField] private bool _isDone;

    [SerializeField] private VoidEventChannelSO _endQuestEvent;

    public int IdQuest => _idQuest;
    public List<StepSO> Steps => _steps;

    public bool IsDone
    {
        get => _isDone;
        set => _isDone = value;
    }

    public VoidEventChannelSO EndQuestEvent => _endQuestEvent;

    public void FinishQuest()
    {
        _isDone = true;
        if (_endQuestEvent != null) _endQuestEvent.RaiseEvent();
    }

    public void SetQuestId(int id)
    {
        _idQuest = id;
    }
}