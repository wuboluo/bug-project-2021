using Bug.Project21.Tools;
using UnityEngine;

[CreateAssetMenu(fileName = "TomatoPool", menuName = "Bug/Pool/TomatoPool")]
public class TomatoPoolSO : ComponentPoolSO<Tomato>
{
    [SerializeField] private TomatoFactorySO factory;

    public override IFactory<Tomato> Factory
    {
        get => factory;
        set => factory = value as TomatoFactorySO;
    }
}