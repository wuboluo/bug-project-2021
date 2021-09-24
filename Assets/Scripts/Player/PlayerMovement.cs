using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Speed = Animator.StringToHash("Speed");
    public float moveSpeed = 5;
    public Animator atkAnimator;

    private Animator animator;
    private Vector2 movement;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat(Horizontal, movement.x);
        animator.SetFloat(Vertical, movement.y);
        animator.SetFloat(Speed, movement.sqrMagnitude);

        if (Input.GetKeyDown(KeyCode.Space)) atkAnimator.SetBool("shoot", true);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }
}