using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 1：装备
/// 2：技能
/// 3：材料
/// </summary>
public enum ItemType
{
    NULL = -1,
    Equip = 0,
    Skill = 1,
    Mater = 2
}
/// <summary>
/// 1：武器
/// 2：铠甲
/// 3：鞋子
/// </summary>
public enum EquipType
{
    NULL = -1,
    Weapon = 0,
    Barde = 1,
    Shoe = 2
}
/// <summary>
/// 1：主动
/// 2：被动
/// </summary>
public enum SkillType
{
    NULL = -1,
    Active = 0,
    Passive = 1
}
/// <summary>
/// 1：
/// </summary>
public enum MaterType
{
    NULL = -1,

}
public abstract class ItemBase : MonoBehaviour
{
    public ItemType ItemType_ = ItemType.NULL;

    public EquipType EquipType_ = EquipType.NULL;

    public SkillType SkillType_ = SkillType.NULL;

    public MaterType MaterType_ = MaterType.NULL;

    public Texture2D Icon_ = null;

    public string Name_ = string.Empty;

    public string Des_ = string.Empty;

    public int Atk_ = 0;
    public int Def_ = 0;
    public int HP_ = 0;
    public int Speed_ = 0;

    public int Cost = 0;

    public int SkillValue_ = 0;

    public ItemBase SkillActive_ = null;

    public ItemBase SkillPassive_ = null;

    public UnityEvent OnInitEvent = new UnityEvent();

    public abstract void OnInit();

    public void AddInitEvent(UnityAction unityAction)
    {
        OnInitEvent.AddListener(unityAction);
    }
    public void RemoveInitEvent(UnityAction unityAction)
    {
        OnInitEvent.RemoveListener(unityAction);
    }
    public void OnClear()
    {
        OnInitEvent.RemoveAllListeners();
    }
    private void OnEnable()
    {
        OnInitEvent.AddListener(OnInit);
        OnInitEvent?.Invoke();
    }
    private void OnDisable()
    {
        OnClear();
    }

}
