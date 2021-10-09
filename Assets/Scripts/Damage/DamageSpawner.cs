using UnityEngine;

public class DamageSpawner : MonoBehaviour
{
    public DamagePoolSO damagePool;

    public IntVectorEventChannelSO _setDamageValueAndSetPos;
    private DamagePopup damage;

    public int initialSize;

    void Start()
    {
        damagePool.Prewarm(initialSize);
    }

    private void OnEnable()
    {
        _setDamageValueAndSetPos.OnEventRaised += SetDamageValueAndSetPos;
    }

    private void OnDisable()
    {
        _setDamageValueAndSetPos.OnEventRaised -= SetDamageValueAndSetPos;
    }


    void SetDamageValueAndSetPos(int value, Vector3 pos)
    {
        damage = damagePool.Request();
        damage.damagePool = damagePool;

        // damage.transform.SetParent(transform);
        damage.SetUp(value);

        // var screenPos = CameraPosSwitcher.i.ToScreenPos(pos);
        // damage.transform.localPosition =
        //     new Vector3(screenPos.x - Screen.width / 2, screenPos.y - Screen.height / 2, 0)
        //     + new Vector3(0, 75);

        damage.transform.position = pos;
    }
}