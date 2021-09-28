using UnityEngine;

namespace Bug.Project21.Quest
{
    public class QuestRequester : MonoBehaviour
    {
        public QuestEventChannelSO requestQuestEvent;
        public QuestSO currentQuest;


        /// <summary>
        /// 请求一个任务，true为新接任务，false为当前已存在任务
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public void RequestOneQuest(GameObject npc)
        {
            if (currentQuest == null)
                requestQuestEvent.Raise(npc.GetComponent<QuestPusher>());
            else
                print($"{gameObject.name} has a quest now");
        }
    }
}