using TMPro;
using UnityEngine;

public class PlayerInfoPanelView : MonoBehaviour
{
    public PlayerDataEventChannelSO _updateDataInPanel;

    public TextMeshProUGUI hp;
    public TextMeshProUGUI atk;
    // ...等等

    private void OnEnable()
    {
        _updateDataInPanel.OnEventRaised += UpdatePlayerDataInPanel;
    }

    private void OnDisable()
    {
        _updateDataInPanel.OnEventRaised -= UpdatePlayerDataInPanel;
    }

    void UpdatePlayerDataInPanel(params int[] datas)
    {
        hp.text = datas[0].ToString();
        atk.text = datas[1].ToString();
        // ...等等
    }
}