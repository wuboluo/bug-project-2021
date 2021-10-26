using UnityEngine;

public interface IInteractor
{
    void OnNearTriggerChange(bool entered, GameObject who);
}