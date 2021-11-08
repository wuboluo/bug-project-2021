using UnityEngine;
using UnityEngine.Events;

public interface ICharacter
{
    void OnHurt(string name, Vector3 dir, float value);
    void OnHitBack(Vector3 dir);
    void OnResumeHP();
    void OnDeath();
    
    event UnityAction<float> updateHpBarEvent;
}