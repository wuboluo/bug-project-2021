using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public abstract class RoleDataModelBase<T> : MonoBehaviour where T : MonoBehaviour
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

    public List<GridBase> EquipLists = new List<GridBase>();

    public List<GridBase> SkillLists = new List<GridBase>();

    public RawImage HeadIcon = null;
    public Text Name_ = null;

    protected int atk_base = 14;
    protected int def_base = 7;
    protected int hp_base = 70;
    protected int speed_base = 1;
    public int atk = 0;
    public int def = 0;
    public int hp = 0;
    public int speed = 0;

    public int GetAtk { get => atk; }
    public int GetDef { get => def; }
    public int GetHP { get => hp; }
    public int GetSpeed { get => speed; }

    public Text Atk_ = null;
    public Text Def_ = null;
    public Text HP_ = null;
    public Text Speed_ = null;

    protected UnityEvent OnInitEvent = new UnityEvent();
    protected UnityEvent OnUIEventListener = new UnityEvent();

    #region 初始化事件加减方法
    public void OnAddInitEvent(UnityAction unityAction)
    {
        OnInitEvent?.AddListener(unityAction);
    }
    public void OnRemoveInitEvent(UnityAction unityAction)
    {
        OnInitEvent?.RemoveListener(unityAction);
    }
    public void OnClearAllInitEvent()
    {
        OnInitEvent?.RemoveAllListeners();
    } 
    #endregion
    public abstract void OnUIDataUpdate();
    public void OnUIEventUpdate()
    {
        OnUIEventListener?.Invoke();
    }
    #region 初始化事件加减方法
    public void OnAddUIEvent(UnityAction unityAction)
    {
        OnUIEventListener?.AddListener(unityAction);
    }
    public void OnRemoveUIEvent(UnityAction unityAction)
    {
        OnUIEventListener?.RemoveListener(unityAction);
    }
    public void OnClearAllUIEvent()
    {
        OnUIEventListener?.RemoveAllListeners();
    } 
    #endregion
    private void Start()
    {
        OnInitEvent?.Invoke();
        OnUIEventListener?.Invoke();
    }

    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        OnClearAllInitEvent();
    }
    public abstract T GetData();
}
