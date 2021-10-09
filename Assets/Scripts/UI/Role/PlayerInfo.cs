public class PlayerInfo : RoleDataModelBase<PlayerInfo>
{
    private void Awake()
    {
        OnAddInitEvent(() => { });
        OnAddUIEvent(OnUIDataUpdate);
        OnUIEventUpdate();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public override PlayerInfo GetData()
    {
        return this;
    }

    public override void OnUIDataUpdate()
    {
        atk = 0;
        def = 0;
        hp = 0;
        speed = 0;
        SkillLists.ForEach(s =>
        {
            if (s.transform.childCount > 0) Destroy(s.transform.GetChild(0).gameObject);
        });
        Invoke("OnInputEquip", 0.05f);
    }

    private void OnInputEquip()
    {
        foreach (var item in EquipLists)
            if (item.transform.childCount > 0 && item.transform.GetChild(0).GetComponent<ItemBase>())
            {
                var data = item.transform.GetChild(0).GetComponent<ItemBase>();
                atk += data.Atk_;
                def += data.Def_;
                hp += data.HP_;
                speed += data.Speed_;
                if (data.SkillActive_ != null)
                {
                    var skillActiveGrid = SkillLists.Find(s => s.transform.childCount <= 0);
                    skillActiveGrid.OnInputTo(data.SkillActive_);
                }

                if (data.SkillPassive_ != null)
                {
                    var skillPassiveGrid = SkillLists.Find(s => s.transform.childCount <= 0);
                    skillPassiveGrid.OnInputTo(data.SkillPassive_);
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
}