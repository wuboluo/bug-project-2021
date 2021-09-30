using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Events/UI/Dialogue line Channel")]
public class DialogueLineChannelSO : DescriptionBaseSO
{
	public UnityAction<string, ActorSO> OnEventRaised;

	public void RaiseEvent(string line, ActorSO actor)
	{
		OnEventRaised?.Invoke(line, actor);
	}
}
