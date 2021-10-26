using UnityEngine;
using UnityEngine.Events;

public interface ICharacter
{
    void OnHurt(string _name, Vector3 _dir, float _value);
    void OnHitBack(Vector3 _dir);
    void OnResumeHP();
    void OnDeath();
    
    event UnityAction<float> updateHpBarEvent;
}