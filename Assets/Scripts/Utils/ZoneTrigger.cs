using System;
using UnityEngine;
using UnityEngine.Events;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private BoolEvent enterZone;
    [SerializeField] private LayerMask layers;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & layers) != 0) enterZone.Invoke(true, other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & layers) != 0) enterZone.Invoke(false, other.gameObject);
    }
}

[Serializable]
public class BoolEvent : UnityEvent<bool, GameObject>
{
}