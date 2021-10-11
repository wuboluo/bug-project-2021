using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataModel", menuName = "Bug/Character/Player/Model")]
public class PlayerModelSO : ScriptableObject
{
    public PlayerDataEventChannelSO _notifyControllerOnDataUpdated;

    public int hp;
    public int atk;
    // ..等等

    public void UpdateData()
    {
        // 例如
        hp++;
        atk++;

        var datas = new[] {hp, atk};

        _notifyControllerOnDataUpdated.RaiseEvent(datas);
    }
}