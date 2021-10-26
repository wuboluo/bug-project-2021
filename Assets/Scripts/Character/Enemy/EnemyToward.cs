using UnityEngine;

public class EnemyToward : MonoBehaviour
{
    [HideInInspector] public float toward;
    private Vector3 startScale, hpScale;

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
            transform.localScale = new Vector3(-startScale.x, startScale.y, 1);
            hpBar.localScale = new Vector3(-hpScale.x, hpScale.y, 1);
        }
        else if (toward <= -0.01f)
        {
            transform.localScale = startScale;
            hpBar.localScale = hpScale;
        }
    }
}