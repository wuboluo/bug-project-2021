using System;
using Pathfinding;
using UnityEngine;
using static CameraPosSwitcher;

namespace Bug.Project21.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public Vector2 mousePos;
        public Transform moveTargetTile;
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

            if (Input.GetMouseButtonDown(0))
            {
                mousePos = i.ToWorldPos(Input.mousePosition);
                moveTargetTile.position = new Vector2((int) Math.Ceiling(mousePos.x), (int) Math.Ceiling(mousePos.y));
            }
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