using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletVFXPool", menuName = "Bug/Pool/BulletVFXPool")]
public class BulletVFXPoolSO : ComponentPoolSO<BulletVFX>
{
    [SerializeField] private BulletFactorySO factory;

    public override IFactory<BulletVFX> Factory
    {
        get => factory;
        set => factory = value as BulletFactorySO;
    }
}