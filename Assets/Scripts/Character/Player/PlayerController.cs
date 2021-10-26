using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour, ICharacter
{
    public PlayerModelSO playerModel;
    public PlayerDataEventChannelSO _onModelChanged;
    public PlayerDataEventChannelSO _updateView;

    public int attackValue;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) playerModel.UpdateData();
    }

    private void OnEnable()
    {
        _onModelChanged.OnEventRaised += NotifyView;
    }

    private void OnDisable()
    {
        _onModelChanged.OnEventRaised -= NotifyView;
    }

    public void OnHurt(string _name, Vector3 _dir, float _value)
    {
    }

    public void OnHitBack(Vector3 _dir)
    {
    }

    public void OnResumeHP()
    {
    }

    public void OnDeath()
    {
    }

    public event UnityAction<float> updateHpBarEvent;

    private void NotifyView(params int[] datas)
    {
        _updateView.RaiseEvent(datas);
    }
}