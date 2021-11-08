using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public PlayerInputControl Controls;
        public float moveSpeed = 3;

        private Vector2 moveInput;
        private Animator animator;

        private Rigidbody2D rb;
        private float stopX, stopY;

        public bool isMove;

        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int InputX = Animator.StringToHash("InputX");
        private static readonly int InputY = Animator.StringToHash("InputY");
        private PlayerInteractor playerInteractor;

        private void Awake()
        {
            Controls = new PlayerInputControl();
        }

        private void Start()
        {
            playerInteractor = GetComponent<PlayerInteractor>();
            rb = GetComponent<Rigidbody2D>();

            animator = GetComponent<Animator>();
        }

        private void OnEnable() => Controls.Enable();

        private void OnDisable() => Controls.Disable();

        private void Update()
        {
            moveInput = Controls.Player.Move.ReadValue<Vector2>();

            if (moveInput != Vector2.zero)
            {
                stopX = moveInput.x;
                stopY = moveInput.y;
                
                playerInteractor.SwitchInteractColliderDirection(moveInput);
            }

            animator.SetBool(IsMoving, moveInput != Vector2.zero);

            animator.SetFloat(InputX, stopX);
            animator.SetFloat(InputY, stopY);
        }

        private void FixedUpdate()
        {
            if (isMove)
            {
                rb.velocity = moveInput * moveSpeed;
            }
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