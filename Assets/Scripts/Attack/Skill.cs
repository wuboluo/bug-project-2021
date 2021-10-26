using Bug.Project21.Skills;
using Cinemachine;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public SkillSO skillData;
    public PlayerModelSO playerData;
    
    public BulletVFXPoolSO bulletVFXPool;
    public OnHitEnemyEventChannelSO _OnHitEnemyEvent;

    private Rigidbody2D rb;
    private CinemachineCollisionImpulseSource vCamera;

    private float damage => skillData.Value + playerData.atk * skillData.Percent;
    private float lifeTime => skillData.LifeTime;
    private float speed => skillData.Speed;
    private bool canPenetrate => skillData.CanPenetrate;
    public bool isShoot => skillData.IsShoot;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (other.CompareTag("Enemy") && !other.GetComponent<Enemy>().IsDeath)
        {
            if (!canPenetrate)
            {
                rb.velocity = Vector2.zero;
                GetComponent<Animator>().Play("OnHit");
            }

            _OnHitEnemyEvent?.RaiseEvent(other.name, other.transform.position - transform.position, damage);

            // 在 Tomato<...Impulse Source>上设置 TriggerObjectFilter.LayerMask = Nothing
            // 否则不需要此行代码，则可以在碰撞指定层的物体时自动震动屏幕
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
    }
}