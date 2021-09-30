using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bug.Project21.Props;

public class Item : ItemBase
{
    public override void OnInit()
    {
        //Dictionary<string, int> values = PropDataSO_.Attrs.GetValues(Props.PropTag.weapon);
        //Dictionary<string, bool> is_values = PropDataSO_.Attrs.GetAbles(Props.PropTag.weapon);
        //values["atk"].ToString();
        if(PropDataSO_!= null)
        {
            PropDataSO_.Attrs.SetValues(PropTag.weapon);

            Icon_ = PropDataSO_.Icon;
            Name_ = PropDataSO_.Name;

            Des_ = PropDataSO_.Describe;

            var values = PropDataSO_.Attrs.GetValues(PropTag.weapon);

            Atk_ = values["atk"];
            Def_ = values["def"];
            HP_ = values["hp"];
            Speed_ = values["speed"];

            Cost = PropDataSO_.Price;

            if (PropDataSO_.skillDataSO.SkillAttrs.Length > 0)
            {
                SkillActive_.SkillDataSO_ = PropDataSO_.skillDataSO.SkillAttrs[0];
                SkillActive_.Name_ = PropDataSO_.skillDataSO.Name;
                SkillActive_.Des_ = PropDataSO_.skillDataSO.SkillDescribe;
            }
        }

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
