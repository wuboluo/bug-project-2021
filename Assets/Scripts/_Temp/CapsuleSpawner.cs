using Bug.Project21.Tools;
using UnityEngine;

public class CapsuleSpawner : MonoBehaviour
{
    public CapsulePoolSO capsulePool;
    public int initialSize;

    private void Start()
    {
        capsulePool.Prewarm(initialSize);
    }

    public void SpawnACapsule()
    {
        var capsule = capsulePool.Request();
        capsule.capsulePool = capsulePool;

        capsule.transform.position = transform.position + Vector3.up;
        capsule.GetComponent<Rigidbody>().velocity = Vector3.up * 5 + Vector3.forward * Random.Range(-1, 1);
        capsule.GetComponent<Rigidbody>().angularVelocity = Vector3.up * 20 + Vector3.left * 20;
    }
}