using Cinemachine;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public TomatoPoolSO tomatoPool;
    public int lifeTime = 3;
    public float speed;

    public VoidEventChannelSO _onHitMonsterEvent;
    public VectorEventChannelSO _hitbackDirectionEvent;

    public ParticleSystem boomParticle;
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

    private void Update()
    {
        transform.Rotate(Vector3.forward * 3);
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
        if (other.CompareTag("Monster") && !other.GetComponent<Monster>().IsDeath)
        {
            DestroySelf();
            _onHitMonsterEvent?.RaiseEvent();
            _hitbackDirectionEvent?.RaiseEvent(other.transform.position - transform.position);

            // 在 Tomato<...Impulse Source>上设置 TriggerObjectFilter.LayerMask = Nothing
            // 否则不需要此行代码，则可以在碰撞太 指定层的物体时自动震动屏幕
            vCamera.GenerateImpulse();
        }
    }

    public void SetSpeed(Vector2 direction)
    {
        rb.velocity = direction * speed;
    }

    private void DestroySelf()
    {
        tomatoPool?.Return(this);
    }
}