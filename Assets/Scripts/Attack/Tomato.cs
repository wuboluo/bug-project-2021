using System;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public TomatoPoolSO tomatoPool;
    public int lifeTime = 3;
    public float speed;
    private Rigidbody2D rb;
    
    public VoidEventChannelSO _onHitMonsterEvent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        if (other.CompareTag("Monster"))
        {
            DestroySelf();
            _onHitMonsterEvent?.RaiseEvent();
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