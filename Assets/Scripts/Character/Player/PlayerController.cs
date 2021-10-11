using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerModelSO playerModel;
    public PlayerDataEventChannelSO _onModelChanged;
    public PlayerDataEventChannelSO _updateView;

    private void OnEnable()
    {
        _onModelChanged.OnEventRaised += NotifyView;
    }

    private void OnDisable()
    {
        _onModelChanged.OnEventRaised -= NotifyView;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerModel.UpdateData();
        }
    }

    private void NotifyView(params int[] datas)
    {
        print(">zzzzz");
        _updateView.RaiseEvent(datas);
    }
}