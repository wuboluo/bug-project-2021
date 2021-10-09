using UnityEngine;

namespace Bug.Project21.Tools
{
    [CreateAssetMenu(fileName = "CapsulePool", menuName = "Bug/Pool/Test-CapsulePool")]
    public class CapsulePoolSO : ComponentPoolSO<ACapsule>
    {
        [SerializeField] private CapsuleFactorySO factory;

        public override IFactory<ACapsule> Factory
        {
            get => factory;
            set => factory = value as CapsuleFactorySO;
        }
    }
}