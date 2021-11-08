using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bug.Project21.Props
{
    [CreateAssetMenu(menuName = "Bug/Props/PropDropperSO")]
    public class PropDropperSO : ScriptableObject
    {
        [SerializeField] private List<GroupOfDrops> gods = new List<GroupOfDrops>();

        public void Drop()
        {
            var dice = UnityEngine.Random.value;
            var rate = 0f;

            foreach (var item in gods)
            {
                rate += item.Probability;
                if (rate >= dice)
                {
                    Debug.Log(item.DropProp.PropName);
                    break;
                }
            }
        }
    }

    [Serializable]
    public class GroupOfDrops
    {
        [SerializeField] private PropSO dropProp;
        [Range(0, 1)] [SerializeField] private float probability;


        public PropSO DropProp => dropProp;
        public float Probability => probability;
    }
}