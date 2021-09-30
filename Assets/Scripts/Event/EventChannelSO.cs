using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Event Channel")]
public class EventChannelSO : ScriptableObject
{
    public UnityAction DoSth;

    public void Raise()
    {
        DoSth?.Invoke();
    }
}