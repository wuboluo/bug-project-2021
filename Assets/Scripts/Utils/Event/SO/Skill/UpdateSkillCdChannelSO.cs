using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Skill/Update CdView On Shoot")]
public class UpdateSkillCdChannelSO : DescriptionBaseSO
{
    public event UnityAction<int, float> OnEventRaised;

    public void RaiseEvent(int index, float cd)
    {
        OnEventRaised?.Invoke(index, cd);
    }
}