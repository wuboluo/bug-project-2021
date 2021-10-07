using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterModelSO monsterModel;

    public VoidEventChannelSO _onHurtEvent;
    public VoidEventChannelSO _onDeathEvent;

    private void OnEnable()
    {
        _onHurtEvent.OnEventRaised += monsterModel.OnHurt;
        _onDeathEvent.OnEventRaised += OnDeath;

        monsterModel.hp = 3;
    }

    private void OnDisable()
    {
        _onHurtEvent.OnEventRaised -= monsterModel.OnHurt;
        _onDeathEvent.OnEventRaised -= OnDeath;
    }

    void OnDeath()
    {
        GetComponent<Animator>().SetBool("isdeath", true);
    }

    public void DeathEvent()
    {
        gameObject.SetActive(false);
    }
}