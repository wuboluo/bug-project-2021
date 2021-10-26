using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[CreateAssetMenu(menuName = "Bug/Attack/GeneralAtk")]
public class GeneralAtkDataSO : ScriptableObject
{
    public int attackValue;
    public OnHitEnemyEventChannelSO _OnHitEnemyEvent;

    [SerializeField] private Vector2 posUp, posDown, posLeft, posRight;
    [SerializeField] private Vector2 scaleV, scaleH;

    public Dictionary<string, (Vector2, Vector2)> atkColliderPos;

    public void Init()
    {
        atkColliderPos = new Dictionary<string, (Vector2, Vector2)>();
        atkColliderPos.Add("Up", (posUp, scaleV));
        atkColliderPos.Add("Down", (posDown, scaleV));
        atkColliderPos.Add("Left", (posLeft, scaleH));
        atkColliderPos.Add("Right", (posRight, scaleH));
    }

    public void Hit(Collider2D atkTarget, Vector3 pos, Transform gun)
    {
        _OnHitEnemyEvent?.RaiseEvent(atkTarget.name, atkTarget.transform.position - gun.position, attackValue);

        gun.GetComponentInChildren<CinemachineCollisionImpulseSource>().GenerateImpulse();
    }
}