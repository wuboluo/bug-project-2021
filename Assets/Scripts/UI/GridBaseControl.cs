using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// 格子位置:
/// 1：背包
/// 2：装备
/// 3：技能
/// 4：商店
/// </summary>
public enum GridPoint
{
    NULL = -1,
    Backpack = 0,
    Equip = 1,
    Skill = 2,
    Store = 3,
}

public abstract class GridBaseControl<T> : MonoBehaviour where T :MonoBehaviour
{
    protected static T _instance = null;
    protected static readonly object _locker = new object();
    public static T GetInstance
    {
        get
        {
            if(_instance == null)
            {
                lock (_locker)
                {
                    if(_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                    }
                }
            }
            return _instance;
        }
    }

    public GridPoint GridPoint_ = GridPoint.NULL;

    public List<GridBase> GridBaseLists = new List<GridBase>();

    public UnityEvent OnInitEvent = new UnityEvent();

    public void OnAddEvent(UnityAction unityAction)
    {
        OnInitEvent.AddListener(unityAction);
    }

    public void OnRemoveEvent(UnityAction unityAction)
    {
        OnInitEvent.RemoveListener(unityAction);
    }

    public void OnClear()
    {
        OnInitEvent.RemoveAllListeners();
    }
    /// <summary>
    /// 生成
    /// </summary>
    public abstract void OnCreateTo();

    private void OnEnable()
    {
        OnInitEvent.AddListener(() =>
        {
            foreach (GridBase item in GridBaseLists)
            {
                item.GridPoint_ = GridPoint_;
            }
        });
        OnInitEvent?.Invoke();
    }

    private void OnDisable()
    {
        OnClear();
    }

}
