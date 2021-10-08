using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterModelSO monsterModel;

    public VoidEventChannelSO _onHurtEvent;
    public VoidEventChannelSO _onDeathEvent;

    public VectorEventChannelSO _hitbackDirectionEvent;

    private Rigidbody2D rb;
    public float strength = 10;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _onHurtEvent.OnEventRaised += OnHurt;
        _onDeathEvent.OnEventRaised += OnDeath;
        _hitbackDirectionEvent.OnEventRaised += OnHitBack;

        monsterModel.hp = 10;
    }

    private void OnDisable()
    {
        _onHurtEvent.OnEventRaised -= OnHurt;
        _onDeathEvent.OnEventRaised -= OnDeath;
        _hitbackDirectionEvent.OnEventRaised -= OnHitBack;
    }

    void OnHurt()
    {
        monsterModel.OnHurtHpChange();
    }

    void OnHitBack(Vector3 v3)
    {
        if (rb != null) rb.AddForce(v3 * strength, ForceMode2D.Impulse);
    }

    void OnDeath()
    {
        GetComponent<Animator>().SetBool("isdeath", true);
    }

    public void DeathEvent()
    {
        gameObject.SetActive(false);
    }
}