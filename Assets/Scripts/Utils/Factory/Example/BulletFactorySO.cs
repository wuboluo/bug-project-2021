using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Bug/Factory/BulletFactorySO")]
public class BulletFactorySO : FactorySO<Skill>
{
    public Skill bullet;

    public override Skill Create()
    {
        return Instantiate(bullet);
    }
}