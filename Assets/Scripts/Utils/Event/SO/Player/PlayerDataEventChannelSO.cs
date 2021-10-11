using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Player/PlayerData Event Channel")]
public class PlayerDataEventChannelSO : ScriptableObject
{
    public UnityAction<int[]> OnEventRaised;

    public void RaiseEvent(params int[] value)
    {
        OnEventRaised?.Invoke(value);
    }
}