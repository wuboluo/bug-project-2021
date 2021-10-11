using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "Factory", menuName = "Bug/Factory/Test-CapsuleFactory")]
public class CapsuleFactorySO : FactorySO<ACapsule>
{
    public ACapsule randomColorCapsule;

    public override ACapsule Create()
    {
        return Instantiate(randomColorCapsule);
    }
}