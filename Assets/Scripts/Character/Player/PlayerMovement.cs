using Pathfinding;
using Sirenix.Utilities;
using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool useAnim;

        private Animator animator;
        private AIDestinationSetter aiSetter;

        public PlayerInputControl controls;

        private float stopX, stopY;

        private void Awake()
        {
            controls = new PlayerInputControl();
        }

        private void Start()
        {
            aiSetter = GetComponent<AIDestinationSetter>();

            if (useAnim) animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var targetPos = aiSetter.target.position - transform.position;

            if (!aiSetter.ai.reachedEndOfPath)
            {
                stopX = targetPos.x;
                stopY = targetPos.y;
            }

            animator.SetBool("Reached", aiSetter.ai.reachedEndOfPath);

            animator.SetFloat("InputX", stopX);
            animator.SetFloat("InputY", stopY);
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }
    }
}