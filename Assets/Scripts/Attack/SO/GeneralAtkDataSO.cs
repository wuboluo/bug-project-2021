using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bug/Attack/GeneralAtk")]
public class GeneralAtkDataSO : ScriptableObject
{
    [SerializeField] private Vector2 posUp, posDown, posLeft, posRight;
    [Space]
    [SerializeField] private Vector2 scaleV;
    [SerializeField] private Vector2 scaleH;

    public Dictionary<string, (Vector2, Vector2)> atkColliderPos;

    public void Init()
    {
        atkColliderPos = new Dictionary<string, (Vector2, Vector2)>();
        atkColliderPos.Add("Up", (posUp, scaleV));
        atkColliderPos.Add("Down", (posDown, scaleV));
        atkColliderPos.Add("Left", (posLeft, scaleH));
        atkColliderPos.Add("Right", (posRight, scaleH));
    }
}