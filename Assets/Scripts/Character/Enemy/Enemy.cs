using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public EnemySO enemyDefaultData;
    [HideInInspector] public int currentHp;

    public float strength = 10;

    public OnHitEnemyEventChannelSO _onHitEnemyEvent;
    public IntVectorEventChannelSO _showDamagePopUpEvent;

    public event UnityAction<float> updateHpBarEvent;

    private Rigidbody2D rb;

    public Transform Player { get; private set; }

    public bool IsDeath { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player").transform;
    }

    private void OnEnable()
    {
        _onHitEnemyEvent.OnEventRaised += OnHurt;
        currentHp = enemyDefaultData.maxHp;
    }

    private void OnDisable()
    {
        _onHitEnemyEvent.OnEventRaised -= OnHurt;
    }

    private void OnHurt(string eName, Vector3 pos)
    {
        if (!eName.Equals(name)) return;

        var tempValue = Random.Range(1, 5);
        currentHp -= tempValue;

        _showDamagePopUpEvent?.RaiseEvent(tempValue, transform.localPosition);
        updateHpBarEvent?.Invoke((float) currentHp / enemyDefaultData.maxHp);
        OnHitBack(pos);

        if (currentHp <= 0) OnDeath();
        GetComponent<Animator>().Play("E1-OnHurt");
    }

    private void OnHitBack(Vector3 v3)
    {
        if (rb != null) rb.AddForce(v3 * strength, ForceMode2D.Impulse);
    }

    public void OnResumeHP()
    {
        if (currentHp >= enemyDefaultData.maxHp) return;
        currentHp++;
        updateHpBarEvent?.Invoke((float) currentHp / enemyDefaultData.maxHp);
    }

    private void OnDeath()
    {
        IsDeath = true;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<EnemyFSM>().enabled = false;
        GetComponent<Animator>().SetBool("_HurtToDeath", true);
    }

    // 绑定在 Death动画最后一帧
    public void DisappearSelf()
    {
        gameObject.SetActive(false);
    }
}