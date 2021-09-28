using Bug.Project21.Tools;
using UnityEngine;

public class ACapsule : MonoBehaviour
{
    public CapsulePoolSO capsulePool;
    public int lifeTime = 3;

    private void OnEnable()
    {
        Invoke(nameof(DestroySelf), lifeTime);
    }

    private void DestroySelf()
    {
        if (capsulePool != null) capsulePool.Return(this);
    }
}