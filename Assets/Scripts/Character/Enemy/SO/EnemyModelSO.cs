using UnityEngine;

[CreateAssetMenu(fileName = "new EnemyModelSO", menuName = "Bug/Enemy/New Enemy")]
public class EnemyModelSO : ScriptableObject
{
    public int maxHp;
    public int currentHp;

    public VoidEventChannelSO _onHpToZeroEvent;
    public IntVectorEventChannelSO _showDamagePopUpEvent;
    public FloatEventChannelSO _currentHpUpdateOnUIEvent;

    public void OnHurtHpChange(Vector3 pos)
    {
        var tempValue = Random.Range(1, 5);
        currentHp -= tempValue;
        _showDamagePopUpEvent?.RaiseEvent(tempValue, pos);
        _currentHpUpdateOnUIEvent?.RaiseEvent((float) currentHp / maxHp);

        if (currentHp <= 0)
            _onHpToZeroEvent?.RaiseEvent();
    }
}