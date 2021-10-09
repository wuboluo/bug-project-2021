using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "DamagePool", menuName = "Bug/Pool/DamagePool")]
public class DamagePoolSO : ComponentPoolSO<DamagePopup>
{
    [SerializeField] private DamageFactorySO factory;

    public override IFactory<DamagePopup> Factory
    {
        get => factory;
        set => factory = value as DamageFactorySO;
    }
}