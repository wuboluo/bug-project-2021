using UnityEngine;
using UnityEngine.Events;

public class Enemy : Unit2D, ICharacter
{
    public EnemySO enemyDefaultData;
    public int currentHp;

    public float strength = 10;
    public bool isStable;

    public OnHitEnemyEventChannelSO _onHitEnemyEvent;
    public IntVectorEventChannelSO _showDamagePopUpEvent;

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

    public event UnityAction<float> updateHpBarEvent;

    public void OnHurt(string eName, Vector3 pos, float value)
    {
        if (!eName.Equals(name)) return;

        currentHp -= (int) value;

        _showDamagePopUpEvent?.RaiseEvent((int) value, transform.localPosition);
        updateHpBarEvent?.Invoke((float) currentHp / enemyDefaultData.maxHp);

        if (!isStable) OnHitBack(pos);

        if (currentHp <= 0) OnDeath();
        GetComponent<Animator>().Play("OnHurt");
    }

    public void OnHitBack(Vector3 v3)
    {
        if (rb != null) rb.AddForce(v3 * strength, ForceMode2D.Impulse);
    }

    public void OnResumeHP()
    {
        if (currentHp >= enemyDefaultData.maxHp) return;
        currentHp++;
        updateHpBarEvent?.Invoke((float) currentHp / enemyDefaultData.maxHp);
    }

    public void OnDeath()
    {
        IsDeath = true;
        GetComponent<Animator>().SetBool("_HurtToDeath", true);

        // 掉落道具
        GetComponent<EnemyInteractor>().propDropper.Drop();
        
        if (isStable) return;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<EnemyFSM>().enabled = false;
    }

    // 绑定在 Death动画最后一帧
    public void DisappearSelf()
    {
        gameObject.SetActive(false);
    }
}