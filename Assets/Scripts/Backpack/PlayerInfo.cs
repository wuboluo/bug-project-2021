using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : SingleMono<PlayerInfo>
{
    public RawImage HeadIcon_ = null;
    public Text NameText_ = null;

    public Text Atk_ = null;
    public Text Def_ = null;
    public Text HP_ = null;
    public Text MoveSpeed_ = null;

    public List<GameObject> EquipGroup = new List<GameObject>();
    public List<GameObject> SkillGroup = new List<GameObject>();

    public void Awake()
    {
        HeadIcon_.texture = DataModel.GetInstance.Icon_;
        NameText_.text = DataModel.GetInstance.Name_;

        Atk_.text = "攻击：" + DataModel.GetInstance.GetPlayerData.Atk_;
        Def_.text = "防御：" + DataModel.GetInstance.GetPlayerData.Def_;
        HP_.text = "生命：" + DataModel.GetInstance.GetPlayerData.HP_;
        MoveSpeed_.text = "移动速度：" + DataModel.GetInstance.GetPlayerData.MoveSpeed_;
    }
    private void FixedUpdate()
    {
    }

    private void LateUpdate()
    {
    }

}