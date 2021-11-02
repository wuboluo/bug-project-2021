using UnityEngine;

public class DamageSpawner : MonoBehaviour
{
    public DamagePoolSO damagePool;

    public IntVectorEventChannelSO _setDamageValueAndSetPos;

    private DamagePopup damage;

    private void Start()
    {
        damagePool.Prewarm(damagePool.size);
    }

    private void OnEnable()
    {
        _setDamageValueAndSetPos.OnEventRaised += SetDamageValueAndSetPos;
    }

    private void OnDisable()
    {
        _setDamageValueAndSetPos.OnEventRaised -= SetDamageValueAndSetPos;
    }


    private void SetDamageValueAndSetPos(int value, Vector3 pos)
    {
        damage = damagePool.Request();
        damage.damagePool = damagePool;

        damage.SetUp(value);
        damage.transform.position = pos;
    }
}