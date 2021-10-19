using Cinemachine;
using UnityEngine;

public class BulletVFX : MonoBehaviour
{
    public BulletVFXPoolSO bulletVFXPool;
    public float lifeTime = 3;
    public float speed;

    public bool isShoot;
    public bool canPenetrate;

    public OnHitEnemyEventChannelSO _onHitMonsterEvent;

    private Rigidbody2D rb;

    private CinemachineCollisionImpulseSource vCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        vCamera = GetComponentInChildren<CinemachineCollisionImpulseSource>();
    }

    private void OnEnable()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(DestroySelf));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster") && !other.GetComponent<Enemy>().IsDeath)
        {
            if (!canPenetrate)
            {
                rb.velocity = Vector2.zero;
                GetComponent<Animator>().Play("OnHit");
            }

            _onHitMonsterEvent?.RaiseEvent(other.name, other.transform.position - transform.position);

            // 在 Tomato<...Impulse Source>上设置 TriggerObjectFilter.LayerMask = Nothing
            // 否则不需要此行代码，则可以在碰撞太 指定层的物体时自动震动屏幕
            vCamera.GenerateImpulse();
        }

        else if (other.CompareTag("Wall") && !canPenetrate)
        {
            rb.velocity = Vector2.zero;
            GetComponent<Animator>().Play("OnHit");
        }
    }

    public void SetSpeed(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    // 击中回收绑定在 击中动画结束事件
    private void DestroySelf()
    {
        bulletVFXPool?.Return(this);

        // // 待定
        // if (!canPenetrate)
        // {
        //     rb.velocity = Vector2.zero;
        //     GetComponent<Animator>().Play("OnHit");
        // }
    }
}