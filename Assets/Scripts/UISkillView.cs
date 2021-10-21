using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class UISkillView : MonoBehaviour
{
    public UpdateSkillCdChannelSO _updateSkillCdOnShoot;
    public SkillInCDChannelSO _skillCDState;

    public Image[] skillIcons;
    private bool[] isCds;

    private float tempCd;

    private void Start()
    {
        skillIcons.ForEach(_ => _.fillAmount = 0);
        isCds = new bool[skillIcons.Length];
    }

    private void Update()
    {
        for (var i = 0; i < isCds.Length; i++)
            if (isCds[i])
            {
                skillIcons[i].fillAmount -= 1 / tempCd * Time.deltaTime;
                if (skillIcons[i].fillAmount <= 0)
                {
                    skillIcons[i].fillAmount = 0;
                    isCds[i] = false;
                    _skillCDState?.RaiseEvent(i, false);
                }
            }
    }

    private void OnEnable()
    {
        _updateSkillCdOnShoot.OnEventRaised += EnterCD;
    }

    private void OnDisable()
    {
        _updateSkillCdOnShoot.OnEventRaised -= EnterCD;
    }

    private void EnterCD(int index, float cd)
    {
        if (isCds[index]) return;
        
        isCds[index] = true;
        _skillCDState?.RaiseEvent(index, true);
        skillIcons[index].fillAmount = 1;

        tempCd = cd;
    }
}