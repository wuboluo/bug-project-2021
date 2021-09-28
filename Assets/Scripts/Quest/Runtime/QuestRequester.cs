using UnityEngine;

namespace Bug.Project21.Quest
{
    public class QuestRequester : MonoBehaviour
    {
        public QuestEventChannelSO requestQuestEvent;
        public QuestSO currentQuest;

        public void RequestOneQuest(GameObject npc)
        {
            if (currentQuest == null)
                requestQuestEvent.Raise(npc.GetComponent<QuestPusher>());
            else
                print($"{gameObject.name} has a quest now");
        }
    }
}