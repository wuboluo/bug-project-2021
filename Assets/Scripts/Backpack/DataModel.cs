using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel {

    /// <summary>
    /// ����
    /// </summary>
    public int Atk_ = 7;
    /// <summary>
    /// ����
    /// </summary>
    public int Def_ = 14;
    /// <summary>
    /// ����
    /// </summary>
    public int HP_ = 70;
    /// <summary>
    /// �ƶ��ٶ�
    /// </summary>
    public int MoveSpeed_ = 1;
}

/// <summary>
/// ��ɫ����ģ��
/// </summary>
public class DataModel : SingleMono<DataModel> {
    /// <summary>
    /// ͷ����ͼ
    /// </summary>
    public Texture2D Icon_ = null;
    /// <summary>
    /// ����
    /// </summary>
    public string Name_ = string.Empty;
    /// <summary>
    /// ����
    /// </summary>
    public string Introduce_ = string.Empty;
    /// <summary>
    /// �������
    /// </summary>
    protected PlayerDataModel PlayerData_ = new PlayerDataModel();
    /// <summary>
    /// �����¼�������
    /// </summary>
    protected UnityEvent UnityEventListener = new UnityEvent();
    public PlayerDataModel GetPlayerData
    {
        get
        {
            return PlayerData_;
        }
    }
    /// <summary>
    /// �����¼�������
    /// </summary>
    public void OnEventUpdateData()
    {
        UnityEventListener?.Invoke();
    }
    /// <summary>
    /// ����¼�
    /// </summary>
    /// <param name="unityAction"></param>
    public void OnAddEvent(UnityAction unityAction)
    {
        UnityEventListener.AddListener(unityAction);
    }
    /// <summary>
    /// �Ƴ��¼�
    /// </summary>
    /// <param name="unityAction"></param>
    public void OnRemoveEvent(UnityAction unityAction)
    {
        UnityEventListener.RemoveListener(unityAction);
    }
    /// <summary>
    /// ���ݸ���
    /// </summary>
    public void OnUpdateDataEvent()
    {
        UnityEventListener?.Invoke();
    }
    /// <summary>
    /// ��ȡ���ݵĽӿ�
    /// </summary>
    public void GetDataSDK(PlayerDataModel _playerData)
    {
        PlayerData_ = _playerData;
    }
    /// <summary>
    /// �������ݵĽӿ�
    /// </summary>
    public void SetDataSDK(PlayerDataModel _playerData)
    {
        PlayerData_ = _playerData;
    }
    /// <summary>
    /// ��������¼�
    /// </summary>
    public void OnClearAllEvent()
    {
        UnityEventListener.RemoveAllListeners();
    }

    private void OnDisable()
    {
        OnClearAllEvent();
    }

    private void OnEnable()
    {
        
    }
}
