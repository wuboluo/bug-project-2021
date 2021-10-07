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

            var dif = other.transform.position - transform.position;
            other.transform.position = new Vector2(other.transform.position.x + dif.x / 2,
                other.transform.position.y + dif.y / 2);

            float explosionStrength = 100;
            rb.AddExplosionForce(explosionStrength, transform.position, 5);
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

public static class Rigidbody2DExt {

    public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force) {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        // Normalize without computing magnitude again
        if (upwardsModifier == 0)
            explosionDir /= explosionDistance;
        else {
            // From Rigidbody.AddExplosionForce doc:
            // If you pass a non-zero value for the upwardsModifier parameter, the direction
            // will be modified by subtracting that value from the Y component of the centre point.
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp((1 - explosionDistance), explosionForce, 0) * explosionDir, mode);
    }
}