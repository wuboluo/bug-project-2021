using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletVFXPool", menuName = "Bug/Pool/BulletVFXPool")]
public class BulletVFXPoolSO : ComponentPoolSO<Skill>
{
    public float cd;
    public bool isCd;

    // 默认为 0，无限距离
    public float castingDistance;

    [SerializeField] private BulletFactorySO factory;

    public override IFactory<Skill> Factory
    {
        get => factory;
        set => factory = value as BulletFactorySO;
    }
}