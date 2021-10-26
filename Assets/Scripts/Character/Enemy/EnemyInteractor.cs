using UnityEngine;

public class EnemyInteractor : MonoBehaviour, IInteractor
{
    public void OnNearTriggerChange(bool entered, GameObject who)
    {
        GetComponent<Animator>().SetBool("FlySwitcher", entered);
    }
}