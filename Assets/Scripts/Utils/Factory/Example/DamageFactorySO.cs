using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Damage", menuName = "Bug/Factory/DamageFactory")]
public class DamageFactorySO : FactorySO<DamagePopup>
{
    public DamagePopup damage;

    public override DamagePopup Create()
    {
        return Instantiate(damage);
    }
}