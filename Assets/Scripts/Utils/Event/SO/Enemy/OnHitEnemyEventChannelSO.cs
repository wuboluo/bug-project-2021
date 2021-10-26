using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Enemy/HitEnemy Event Channel")]
public class OnHitEnemyEventChannelSO : DescriptionBaseSO
{
    public event UnityAction<string, Vector3, float> OnEventRaised;

    public void RaiseEvent(string str, Vector3 pos, float value)
    {
        OnEventRaised?.Invoke(str, pos, value);
    }
}