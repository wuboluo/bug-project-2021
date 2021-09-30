using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bug.Project21.Quest
{
    public class QuestlineManager : MonoBehaviour
    {
        public List<QuestlineSO> questlines = new List<QuestlineSO>();

        public QuestEventChannelSO RequestQuestEventSO;

        private void OnEnable()
        {
            RequestQuestEventSO.beRequestQuest += PushOneQuest;
        }

        private void OnDisable()
        {
            if (RequestQuestEventSO.beRequestQuest != null) RequestQuestEventSO.beRequestQuest -= PushOneQuest;
        }

        public void PushOneQuest(QuestPusher pusher)
        {
            print(pusher.name);

            if (pusher.quest != null)
            {
                print("current npc has be set a quest");
            }
            else
            {
                var idleQuest = questlines.First().quests
                    .Find(_ => _.pusher.name == pusher.name && _.isDone == false);

                print($"questManager push a new quest: {idleQuest.Name}");
                pusher.quest = idleQuest;

                pusher.OnBeAssigned();
            }
        }
    }
}