using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerInputControl controls;
        public float moveSpeed = 3;

        private Vector2 moveInput;
        private Animator animator;

        private Rigidbody2D rb;
        private float stopX, stopY;

        public bool isMove;

        private void Awake()
        {
            controls = new PlayerInputControl();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            animator = GetComponent<Animator>();
        }

        private void OnEnable() => controls.Enable();

        private void OnDisable() => controls.Disable();

        void Update()
        {
            moveInput = controls.Player.Move.ReadValue<Vector2>();

            if (moveInput != Vector2.zero)
            {
                stopX = moveInput.x;
                stopY = moveInput.y;
            }

            animator.SetBool("isMoving", moveInput != Vector2.zero);

            animator.SetFloat("InputX", stopX);
            animator.SetFloat("InputY", stopY);
        }

        private void FixedUpdate()
        {
            if (isMove)
                rb.velocity = moveInput * moveSpeed;
            else
            {
                rb.velocity = Vector2.zero;
            }
        }


        public void SetIsMove()
        {
            isMove = true;
            GetComponentInChildren<Attacker>().generalCollider.GetComponent<Collider2D>().enabled = false;
        }
    }
}