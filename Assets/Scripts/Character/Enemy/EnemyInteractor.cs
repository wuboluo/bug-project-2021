using Bug.Project21.Props;
using UnityEngine;

public class EnemyInteractor : MonoBehaviour, IInteractor
{
    public PropDropperSO propDropper;
    
    public void OnNearTriggerChange(bool entered, GameObject who)
    {
        GetComponent<Animator>().SetBool("FlySwitcher", entered);
    }
}