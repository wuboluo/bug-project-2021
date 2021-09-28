using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : ItemBase
{
    public override void OnInit()
    {
        if(ItemType_!= ItemType.Skill)
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
