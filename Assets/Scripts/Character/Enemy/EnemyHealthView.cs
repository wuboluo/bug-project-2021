using UnityEngine;

public class EnemyHealthView : MonoBehaviour
{
    public Transform hpBar;
    private float maxHpBarLength;

    private void OnEnable()
    {
        transform.GetComponent<Enemy>().updateHpBarEvent += UpdateHpValue;
        maxHpBarLength = hpBar.localScale.x;
    }

    private void OnDisable()
    {
        transform.GetComponent<Enemy>().updateHpBarEvent -= UpdateHpValue;
    }

    private void UpdateHpValue(float value)
    {
        if (value <= 0)
        {
            hpBar.localScale = new Vector3(0, 1, 1);

            // 隐藏生命条
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            hpBar.localScale = new Vector3(maxHpBarLength * value, 1, 1);
        }
    }
}