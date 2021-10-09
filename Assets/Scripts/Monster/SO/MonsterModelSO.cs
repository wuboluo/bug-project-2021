using UnityEngine;

[CreateAssetMenu(fileName = "new MonsterModelSO", menuName = "Bug/Monster/New Monster")]
public class MonsterModelSO : ScriptableObject
{
    public int maxHp;
    public int currentHp;

    public VoidEventChannelSO _onHpToZeroEvent;
    public IntVectorEventChannelSO _showDamagePopUpEvent;
    public FloatEventChannalSO _currentHpUpdateOnUIEvent;

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