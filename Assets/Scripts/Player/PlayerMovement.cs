using UnityEngine;

namespace Bug.Project21.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public float moveSpeed = 3;
        private Camera _camera;

        private Animator animator;

        public PlayerInputControl controls;
        private Rigidbody2D rb;

        private void Awake()
        {
            controls = new PlayerInputControl();
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            _camera = Camera.main;
        }

        private void FixedUpdate()
        {
            // move
            var moveInput = controls.Player.Move.ReadValue<Vector2>();
            rb.velocity = moveInput * moveSpeed;

            // todo: mouse position 
            var mousePos = controls.Player.MousePos.ReadValue<Vector2>();
            var mousePosZ = _camera.farClipPlane * .5f;
            var mouseWorldPos =
                _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)) -
                transform.position;


            // animator
            animator.SetFloat("MousePositionX", mouseWorldPos.x);
            animator.SetFloat("MousePositionY", mouseWorldPos.y);

            animator.SetFloat("Horizontal", moveInput.x);
            animator.SetFloat("Vertical", moveInput.y);

            animator.SetFloat("Speed", moveInput.sqrMagnitude);
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