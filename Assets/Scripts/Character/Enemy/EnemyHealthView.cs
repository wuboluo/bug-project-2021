using UnityEngine;

public class EnemyHealthView : MonoBehaviour
{
    public Transform hpBar;

    public FloatEventChannelSO _updateHpBarValueEvent;
    private float maxHpBarLength;

    private void OnEnable()
    {
        _updateHpBarValueEvent.OnEventRaised += UpdateHpValue;
        maxHpBarLength = hpBar.localScale.x;
    }

    private void OnDisable()
    {
        _updateHpBarValueEvent.OnEventRaised -= UpdateHpValue;
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