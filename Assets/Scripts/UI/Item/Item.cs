using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bug.Project21.Props;

public class Item : ItemBase
{
    public override void OnInit()
    {

        #region 基本属性
        if (Data_PropSO != null)
        {
            Data_PropSO.Attrs.SetValues(PropTag.equip);

            switch (Data_PropSO.Tag)
            {
                case PropTag.stuff:

                    break;
                case PropTag.equip:
                    switch (Data_PropSO.EquipType)
                    {
                        case Bug.Project21.Props.EquipType.weapon:
                            EquipType_ = EquipType.Weapon;
                            break;
                        case Bug.Project21.Props.EquipType.armor:
                            EquipType_ = EquipType.Barde;
                            break;
                        case Bug.Project21.Props.EquipType.shoes:
                            EquipType_ = EquipType.Shoe;
                            break;
                    }
                    break;
            }

            Icon_ = Data_PropSO.Icon;
            Name_ = Data_PropSO.PropName;
            Des_ = Data_PropSO.Describe;

            var datas = Data_PropSO.Attrs.GetValues(PropTag.equip);

            Atk_ = datas["atk"];
            Def_ = datas["def"];
            HP_ = datas["hp"];
            Speed_ = datas["speed"];
            Price_ = Data_PropSO.Price;
        }

        #endregion

        #region 技能属性
        if (transform.childCount > 0)
        {
            ItemBase skill = transform.GetChild(0).GetComponent<ItemBase>();
            skill.transform.localScale = Vector3.zero;
            switch (Data_PropSO.Skill.Type)
            {
                case PropSkillType.active:
                    SkillActive_ = skill;
                    SkillActive_.ItemType_ = ItemType.Skill;
                    SkillActive_.SkillType_ = SkillType.Active;
                    SkillActive_.Icon_ = Data_PropSO.Skill.Icon;
                    SkillActive_.Name_ = Data_PropSO.Skill.Name;
                    SkillActive_.SkillValue_ = Data_PropSO.Skill.Value;
                    SkillActive_.Des_ = Data_PropSO.Skill.SkillDescribe;
                    break;
                case PropSkillType.passive:
                    SkillPassive_ = skill;
                    SkillPassive_.ItemType_ = ItemType.Skill;
                    SkillPassive_.SkillType_ = SkillType.Passive;
                    SkillPassive_.Icon_ = Data_PropSO.Skill.Icon;
                    SkillPassive_.Name_ = Data_PropSO.Skill.Name;
                    SkillPassive_.SkillValue_ = Data_PropSO.Skill.Value;
                    SkillPassive_.Des_ = Data_PropSO.Skill.SkillDescribe;
                    break;
            }
        }

        #endregion


        if (ItemType_!= ItemType.Skill)
        {
            GetComponent<RawImage>().texture = Icon_;
        }
        else
        {
            //if (GetComponent<RawImage>() != null)
            //{
            //    Destroy(GetComponent<RawImage>());
            //}
            GetComponent<RawImage>().texture = Icon_;
            this.transform.localScale = Vector3.zero;
        }
    }
}
