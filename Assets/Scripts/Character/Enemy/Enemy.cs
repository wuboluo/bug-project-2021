using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyModelSO monsterModel;

    [SerializeField] private float strength = 10;

    public VoidEventChannelSO _onHurtEvent;
    public VoidEventChannelSO _onDeathEvent;
    public VectorEventChannelSO _hitbackDirectionEvent;

    private Rigidbody2D rb;

    public bool IsDeath { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        monsterModel.maxHp = 50;
        monsterModel.currentHp = 50;
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
        GetComponent<Animator>().Play("E1-OnHurt");
    }

    private void OnHitBack(Vector3 v3)
    {
        if (rb != null)
        {
            rb.AddForce(v3 * strength, ForceMode2D.Impulse);
        }
    }

    private void OnDeath() 
    {
        gameObject.SetActive(false);
        IsDeath = true;
    }
}