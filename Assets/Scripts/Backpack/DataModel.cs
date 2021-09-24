using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDataModel {

    /// <summary>
    /// 攻击
    /// </summary>
    public int Atk_ = 7;
    /// <summary>
    /// 防御
    /// </summary>
    public int Def_ = 14;
    /// <summary>
    /// 生命
    /// </summary>
    public int HP_ = 70;
    /// <summary>
    /// 移动速度
    /// </summary>
    public int MoveSpeed_ = 1;
}

/// <summary>
/// 角色数据模型
/// </summary>
public class DataModel : SingleMono<DataModel> {
    /// <summary>
    /// 头像贴图
    /// </summary>
    public Texture2D Icon_ = null;
    /// <summary>
    /// 名称
    /// </summary>
    public string Name_ = string.Empty;
    /// <summary>
    /// 介绍
    /// </summary>
    public string Introduce_ = string.Empty;
    /// <summary>
    /// 玩家数据
    /// </summary>
    protected PlayerDataModel PlayerData_ = new PlayerDataModel();
    /// <summary>
    /// 数据事件监听器
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
    /// 数据事件更新器
    /// </summary>
    public void OnEventUpdateData()
    {
        UnityEventListener?.Invoke();
    }
    /// <summary>
    /// 添加事件
    /// </summary>
    /// <param name="unityAction"></param>
    public void OnAddEvent(UnityAction unityAction)
    {
        UnityEventListener.AddListener(unityAction);
    }
    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="unityAction"></param>
    public void OnRemoveEvent(UnityAction unityAction)
    {
        UnityEventListener.RemoveListener(unityAction);
    }
    /// <summary>
    /// 数据更新
    /// </summary>
    public void OnUpdateDataEvent()
    {
        UnityEventListener?.Invoke();
    }
    /// <summary>
    /// 获取数据的接口
    /// </summary>
    public void GetDataSDK(PlayerDataModel _playerData)
    {
        PlayerData_ = _playerData;
    }
    /// <summary>
    /// 设置数据的接口
    /// </summary>
    public void SetDataSDK(PlayerDataModel _playerData)
    {
        PlayerData_ = _playerData;
    }
    /// <summary>
    /// 清空所有事件
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
