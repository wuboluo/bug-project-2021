using Bug.Project21.Quest;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Quest Event Channel")]
public class QuestEventChannelSO : ScriptableObject
{
    public UnityAction<QuestPusher> beRequestQuest;

    public void Raise(QuestPusher pusher)
    {
        beRequestQuest?.Invoke(pusher);
    }
}