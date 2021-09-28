using UnityEngine;

namespace Bug.Project21.Quest
{
    public class QuestPusher : MonoBehaviour, IQuest
    {
        public QuestSO quest;

        public void OnBeAssigned()
        {
            print($"{gameObject.name} is be assigned a quest from questManager now");

            print(quest.Name);
        }
    }
}