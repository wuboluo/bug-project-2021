using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Skill/Skill is in CD")]
public class SkillInCDChannelSO : DescriptionBaseSO
{
    public event UnityAction<int, bool> OnEventRaised;

    public void RaiseEvent(int index, bool isCd)
    {
        OnEventRaised?.Invoke(index, isCd);
    }
}