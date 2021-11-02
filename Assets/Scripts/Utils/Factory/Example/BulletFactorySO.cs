using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Bug/Factory/BulletFactorySO")]
public class BulletFactorySO : FactorySO<BulletVFX>
{
    public BulletVFX bullet;

    public override BulletVFX Create()
    {
        return Instantiate(bullet);
    }
}