using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterModelSO monsterModel;

    [SerializeField] private float strength = 10;
    [SerializeField] private float moveSpeed;

    public VoidEventChannelSO _onHurtEvent;
    public VoidEventChannelSO _onDeathEvent;
    public VectorEventChannelSO _hitbackDirectionEvent;
    private Transform player;

    private Rigidbody2D rb;

    public bool IsDeath { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;

        monsterModel.maxHp = 50;
        monsterModel.currentHp = 50;
    }

    private void Update()
    {
        if (!IsDeath)
        {
            Vector2 dir = player.position - transform.position;
            dir.Normalize();
            rb.AddForce(dir * moveSpeed);
        }
    }

    private void OnEnable()
    {
        _onHurtEvent.OnEventRaised += OnHurt;
        _onDeathEvent.OnEventRaised += OnDeath;
        _hitbackDirectionEvent.OnEventRaised += OnHitBack;
    }

    private void OnDisable()
    {
        _onHurtEvent.OnEventRaised -= OnHurt;
        _onDeathEvent.OnEventRaised -= OnDeath;
        _hitbackDirectionEvent.OnEventRaised -= OnHitBack;
    }

    private void OnHurt()
    {
        monsterModel.OnHurtHpChange(transform.localPosition);
    }

    private void OnHitBack(Vector3 v3)
    {
        if (rb != null) rb.AddForce(v3 * strength, ForceMode2D.Impulse);
    }

    private void OnDeath()
    {
        GetComponent<Animator>().SetBool("isdeath", true);
        IsDeath = true;
    }

    public void DeathEvent()
    {
        gameObject.SetActive(false);
    }
}