using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//关卡：
//关卡类型（塔防、打野）、
//关卡名称，
//管卡计时器
//怪物（怪物数量、怪物类型）、
//Boss
//胜利失败条件、
//陷阱（陷阱类型）、
//道具掉落（机制待定，掉落几率）、--------------感觉应该放在敌人身上统一化管理
//过关奖励、
//任务、
//任务NPC、

/// <summary>
/// 关卡类型
/// 0：塔防
/// 1：打野
/// </summary>
public enum CustomsPassType
{
    Null = -1,
    TowerDefence = 0, //塔防
    Jungle = 1 //打野
}
/// <summary>
/// 怪物类型
/// 0：主动攻击类型的怪物
/// 1：被动攻击类型的怪物
/// </summary>
public enum EnemyType
{
    Null = -1,
    ActiveEnemy = 0,
    PassiveEnemy = 1
}
/// <summary>
/// 陷阱类型
/// 0：主动陷阱
/// 1：被动陷阱
/// </summary>
public enum TrapType
{
    Null = -1,
    ActiveTrap = 0,
    PassiveTrap = 1
}
public class CustomsPass : MonoBehaviour
{
    /// <summary>
    /// Boss事件
    /// </summary>
    public Action OnBossAiEvent = null;
    public List<GameObject> EnemyAllList = new List<GameObject>();
    /// <summary>
    /// 当前活跃敌人列表
    /// </summary>
    [Tooltip("当前活跃敌人数量")]
    public int CurrentActivityEnemyNumber = 0;
    /// <summary>
    /// 关卡类型 1：塔防 2：打野
    /// </summary>
    [Tooltip("关卡类型-> 1:塔防 2:打野")]
    public CustomsPassType mCustomsPassType = CustomsPassType.Null;
    /// <summary>
    /// 关卡名称
    /// </summary>
    [Tooltip("关卡名称")]
    public string mCustomsPassName = string.Empty;
    /// <summary>
    /// 关卡计时器
    /// </summary>
    [Tooltip("关卡计时器")]
    public float mTimer = 0;
    /// <summary>
    /// 最大时间 超过后算是关卡失败 （单位分钟）
    /// </summary>
    [Tooltip("关卡计时器最大时间")]
    public float mTimerMaxValue = 10;
    /// <summary>
    /// 角色
    /// </summary>
    public GameObject mPlayer = null;
    /// <summary>
    /// 任务NPC
    /// </summary>
    [Tooltip("人物NPC预制体")]
    public GameObject mTaskNpc = null;
    /// <summary>
    /// 任务
    /// </summary>
    [Tooltip("任务")]
    public GameObject mTask = null;
    /// <summary>
    /// 过关奖励列表
    /// </summary>
    [Tooltip("过关奖励预制体列表")]
    public List<GameObject> mAwardLists = new List<GameObject>();
    /// <summary>
    /// 是否胜利
    /// </summary>
    [Tooltip("是否胜利")]
    public bool IsSuccess = false;
    /// <summary>
    /// 关卡状态
    /// </summary>
    [Tooltip("关卡状态")]
    public bool IsStart = false;

    #region 怪物配置器
    /// <summary>
    /// 最大敌人波次
    /// </summary>
    [Tooltip("最大敌人波次")]
    [Header("Enemy配置参数--------------")]
    public int MaxEnemyWavePicking = 12;
    /// <summary>
    /// 当前波次
    /// </summary>
    [Tooltip("当前波次")]
    private int CurrentEnemyWavePicking = 0;
    /// <summary>
    /// 波次间隔时间
    /// </summary>
    [Tooltip("波次生成间隔时间")]
    public float EnemyWavePickingIntervalTimer = 7f;
    /// <summary>
    /// 波次计时器
    /// </summary>
    [Tooltip("波次计时器")]
    public float WavePickingTimer = 0;
    [Header("小怪数量")]
    [Range(1, 12)] public int EnemyCount = 12;
    [Header("间隔距离")]
    [Range(0.7f, 2.1f)] public float EnemyDistance = 1f;
    [Header("无法描述")]
    [Range(0, 0.7f)] public float EnemyGradually = 0;
    public Transform EnemyCreatePoint = null;
    public Transform EnemyPoint = null;
    [Header("怪物预制体")]
    public GameObject EnemyTargetPrefab = null;

    /// <summary>
    /// 生成Enemy配置点
    /// </summary>
    public void OnCreateEnemyGroup()
    {
        GameObject enemyMatrix = Instantiate(EnemyCreatePoint.gameObject, this.transform);
        enemyMatrix.name = nameof(EnemyCreatePoint);
        float angle = 360f / EnemyCount;
        for (int i = 0; i < EnemyCount; i++)
        {
            GameObject enemy = Instantiate(EnemyPoint.gameObject, enemyMatrix.transform);
            enemy.name = nameof(EnemyPoint);
            enemy.transform.localEulerAngles = new Vector3(0, 0, i * angle);
            enemy.transform.Translate(transform.right * (EnemyDistance + (EnemyDistance * i * EnemyGradually)));
        }
    }
    /// <summary>
    /// 隐藏Enemy配置点
    /// </summary>
    public void OnHideEnemyGroup()
    {
        var p = GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(EnemyCreatePoint)) == 0);
        foreach (Transform _EnemyCreatePoint in p)
        {
            foreach (Transform item in _EnemyCreatePoint)
            {
                item.localScale = Vector3.zero;
            }
        }
    }
    /// <summary>
    /// 显示Enemy配置点
    /// </summary>
    public void OnShowEnemyGroup()
    {
        var p = GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(EnemyCreatePoint)) == 0);
        foreach (Transform _EnemyCreatePoint in p)
        {
            foreach (Transform item in _EnemyCreatePoint)
            {
                item.localScale = Vector3.one;
            }
        }
    }

    [Header("Boss配置参数")]
    [Header("Boss配置参数--------------")]
    [Range(1, 3)] public int BossCount = 1;
    [Header("间隔距离")]
    [Range(0.7f, 2.1f)] public float BossDistance = 1f;
    [Header("无法描述")]
    [Range(0, 0.7f)] public float BossGradually = 0;
    public Transform BossCreatePoint = null;
    public Transform BossPoint = null;
    public GameObject BossTargetPrefab = null;

    /// <summary>
    /// 生成Boss配置点
    /// </summary>
    public void OnCreateBossGroup()
    {
        GameObject enemyMatrix = Instantiate(BossCreatePoint.gameObject, this.transform);
        enemyMatrix.name = nameof(BossCreatePoint);
        float angle = 360 / BossCount;
        for (int i = 0; i < BossCount; i++)
        {
            GameObject boss = Instantiate(BossPoint.gameObject, enemyMatrix.transform);
            boss.name = nameof(BossPoint);
            boss.transform.localEulerAngles = new Vector3(0, 0, i * angle);
            boss.transform.Translate(BossCount > 1 ? transform.right * (BossDistance + (BossDistance * i * BossGradually)) : Vector3.zero);
        }
    }
    /// <summary>
    /// 隐藏Boss配置点
    /// </summary>
    public void OnHideBossGroup()
    {
        var p = GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(BossCreatePoint)) == 0);
        foreach (Transform _EnemyCreatePoint in p)
        {
            foreach (Transform item in _EnemyCreatePoint)
            {
                item.localScale = Vector3.zero;
            }
        }
    }
    /// <summary>
    /// 显示Boss配置点
    /// </summary>
    public void OnShowBossGroup()
    {
        var p = GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(BossCreatePoint)) == 0);
        foreach (Transform _EnemyCreatePoint in p)
        {
            foreach (Transform item in _EnemyCreatePoint)
            {
                item.localScale = Vector3.one;
            }
        }
    } 
    #endregion

    /// <summary>
    /// 开始闯关
    /// </summary>
    void OnStart()
    {
        if (!IsStart)
        {
            mTimer = 0;
            IsStart = true;
            CurrentActivityEnemyNumber = 0;
            CurrentEnemyWavePicking = 0;
            WavePickingTimer = 0;

            int enemyGroupCount = GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(EnemyCreatePoint)) == 0).Count;
            int bossGroupCount = GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(BossCreatePoint)) == 0).Count;
            CurrentActivityEnemyNumber = (EnemyCount*enemyGroupCount) * MaxEnemyWavePicking + (BossCount * bossGroupCount);
            OnCreateEnemyAWaveOf();

            OnCreateBoss();
        }
        print("开启关卡成功。。。。。。。");
    }

    /// <summary>
    /// 生成一波敌人
    /// </summary>
    void OnCreateEnemyAWaveOf()
    {
        GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(EnemyCreatePoint)) == 0).ForEach(_EnemyCreatePoint =>
        {
            int count = _EnemyCreatePoint.childCount;
            for (int i = 0; i < count; i++)
            {
                if(_EnemyCreatePoint.GetChild(i).name.CompareTo(nameof(EnemyPoint)) == 0)
                {
                    GameObject _enemy = Instantiate(EnemyTargetPrefab, _EnemyCreatePoint);
                    _enemy.transform.position = _EnemyCreatePoint.GetChild(i).position;
                    EnemyAllList.Add(_enemy);
                }
            }
        });
        CurrentEnemyWavePicking += 1;
        if (CurrentEnemyWavePicking >= MaxEnemyWavePicking)
        {
            OnBossAiEvent?.Invoke();
            print("出发BossAI行为事件");
        }
    }

    /// <summary>
    /// 生成一波Boss
    /// </summary>
    void OnCreateBoss()
    {
        GetComponentsInChildren<Transform>().ToList().FindAll(s => s.name.CompareTo(nameof(BossCreatePoint)) == 0).ForEach(_BossCreatePoint =>
        {
            int count = _BossCreatePoint.childCount;
            for (int i = 0; i < count; i++)
            {
                if (_BossCreatePoint.GetChild(i).name.CompareTo(nameof(BossPoint)) == 0)
                {
                    GameObject _boss = Instantiate(BossTargetPrefab, _BossCreatePoint);
                    _boss.transform.position = _BossCreatePoint.GetChild(i).position;
                    EnemyAllList.Add(_boss);
                }
            }
        });
    }

    /// <summary>
    /// 时间检测器
    /// </summary>
    void OnTimerMonitor()
    {
        if (IsStart)
        {
            if(CurrentEnemyWavePicking < MaxEnemyWavePicking)
            {
                WavePickingTimer += Time.deltaTime;
                if (WavePickingTimer >= EnemyWavePickingIntervalTimer)
                {
                    WavePickingTimer = 0;
                    OnCreateEnemyAWaveOf();
                }
            }
            if (mTimer < mTimerMaxValue)
            {
                mTimer += Time.deltaTime;
                if (mTimer >= mTimerMaxValue)
                {
                    IsStart = false;
                    mTimer = 0;
                    OnLosePage();
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnStart();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (EnemyAllList.Count > 0)
            {
                int index = EnemyAllList.Count - 1;
                GameObject enemy = EnemyAllList[index];
                EnemyAllList.RemoveAt(index);
                Destroy(enemy);
                OnSubEnemyNumber();
                Debug.Log(CurrentActivityEnemyNumber);
            }
        }

        OnTimerMonitor();
    }

    //void OnAddEnemyNumber()
    //{
    //    CurrentActivityEnemyNumber += 1;
    //}
    public void OnSubEnemyNumber()
    {
        CurrentActivityEnemyNumber -= 1;
        if (CurrentActivityEnemyNumber <= 0)
        {
            OnSuccessPage();
            OnSendAward();
        }
    }
    /// <summary>
    /// 发送奖励
    /// </summary>
    void OnSendAward()
    {
        print("发送奖励");
    }
    /// <summary>
    /// 关卡结束返回主地图
    /// </summary>
    void OnGameOver()
    {
        print("管卡结束返回主地图");
    }
    /// <summary>
    /// 胜利页面
    /// </summary>
    void OnSuccessPage()
    {
        print("通关成功");
    }
    /// <summary>
    /// 失败页面
    /// </summary>
    void OnLosePage()
    {
        print("通关失败");
    }
}
