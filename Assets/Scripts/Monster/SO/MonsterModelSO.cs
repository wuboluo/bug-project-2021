using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new MonsterModelSO", menuName = "Bug/Monster/New Monster")]
public class MonsterModelSO : ScriptableObject
{
    public int hp;

    public VoidEventChannelSO _onHpToZeroEvent;

    public void OnHurt()
    {
        hp--;
        Debug.Log(hp);

        if (hp <= 0)
            _onHpToZeroEvent?.RaiseEvent();
    }
}