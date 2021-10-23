using UnityEngine;

namespace Pathfinding
{
    public class AIDestinationSetter : VersionedMonoBehaviour
    {
        public Transform target;
        public IAstarAI ai;

        protected override void Awake()
        {
            base.Awake();
            ai = GetComponent<IAstarAI>();
        }

        void OnEnable()
        {
            if (ai != null) ai.onSearchPath += Update;
        }

        void OnDisable()
        {
            if (ai != null) ai.onSearchPath -= Update;
        }

        void Update()
        {
            if (target != null && ai != null)
            {
                ai.destination = target.position;
            }
        }

        public void UpdateTargetPosition()
        {
            ai.SearchPath();
        }
    }
}