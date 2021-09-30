using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : RoleDataModelBase<PlayerInfo>
{
    private void Awake()
    {
        OnAddInitEvent(()=> { 
        
        });
        OnAddUIEvent(OnUIDataUpdate);
        OnUIEventUpdate();
    }

    public override PlayerInfo GetData()
    {
        return this;
    }

    public override void OnUIDataUpdate()
    {
        SkillLists.ForEach(s =>
        {
            if (s.transform.childCount > 0)
            {
                Destroy(s.transform.GetChild(0).gameObject);
            }
        });
        Invoke("OnInputEquip", 0.05f);
    }

    void OnInputEquip()
    {
        atk = 0;
        def = 0;
        hp = 0;
        speed = 0;
        foreach (GridBase item in EquipLists)
        {
            if (item.transform.childCount > 0 && item.transform.GetChild(0).GetComponent<ItemBase>())
            {
                ItemBase data = item.transform.GetChild(0).GetComponent<ItemBase>();
                atk += data.Atk_;
                def += data.Def_;
                hp += data.HP_;
                speed += data.Speed_;


                if (data.SkillActive_ != null)
                {
                    //GridBase skillActiveGrid = SkillLists.Find(s => s.transform.childCount <= 0);
                    //skillActiveGrid.OnInputTo(data.SkillActive_);
                    (item as EquipGrid).SkillGrids[0].OnInputTo(data.SkillActive_);
                }
                switch ((item as EquipGrid).EquipType_)
                {
                    case EquipType.Weapon:

                        break;
                    case EquipType.Barde:

                        break;
                    case EquipType.Shoe:
                        if (data.SkillPassive_ != null)
                        {
                            //GridBase skillPassiveGrid = SkillLists.Find(s => s.transform.childCount <= 0);
                            //skillPassiveGrid.OnInputTo(data.SkillPassive_);
                            (item as EquipGrid).SkillGrids[1].OnInputTo(data.SkillPassive_);
                        }
                        break;
                }
            }
        }
        atk += atk_base;
        def += def_base;
        hp += hp_base;
        speed += speed_base;

        Atk_.text = "攻击：" + atk;
        Def_.text = "防御：" + def;
        HP_.text = "生命：" + hp;
        Speed_.text = "移动速度：" + speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
