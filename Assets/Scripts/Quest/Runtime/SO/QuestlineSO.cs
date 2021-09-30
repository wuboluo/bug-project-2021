using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Project21.Quest
{
    [CreateAssetMenu(fileName = "New Questline", menuName = "Bug/Questline")]
    public class QuestlineSO : ScriptableObject
    {
        public List<QuestSO> quests = new List<QuestSO>();
    }
}