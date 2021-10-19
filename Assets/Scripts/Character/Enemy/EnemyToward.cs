using UnityEngine;

public class EnemyToward : MonoBehaviour
{
    [HideInInspector] public float toward;
    private Vector2 startScale, hpScale;

    private Transform hpBar;

    private void Start()
    {
        hpBar = transform.GetChild(0);
        startScale = transform.localScale;
        hpScale = hpBar.localScale;
    }

    private void FixedUpdate()
    {
        if (toward >= 0.01f)
        {
            transform.localScale = new Vector2(-startScale.x, startScale.y);
            hpBar.localScale = new Vector2(-hpScale.x, hpScale.y);
        }
        else if (toward <= -0.01f)
        {
            transform.localScale = startScale;
            hpBar.localScale = hpScale;
        }
    }
}