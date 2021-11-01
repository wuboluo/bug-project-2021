using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bug/Event/Backpack/CheckPropForBackpack")]
public class CheckPropForBackpackEventChannelSO : DescriptionScriptableObject
{
    public Func<int, int, bool> OnEventRaised;

    public bool RaiseEvent(int propID, int propAmount)
    {
        var res = OnEventRaised?.Invoke(propID, propAmount);
        return res ?? false;
    }
}