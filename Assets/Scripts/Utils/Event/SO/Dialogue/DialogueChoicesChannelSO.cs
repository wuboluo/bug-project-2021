using System.Collections.Generic;
using Bug.Project21.Dialogue;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Dialogue/Dialogue Choices Channel")]
public class DialogueChoicesChannelSO : DescriptionBaseSO
{
	public UnityAction<List<DialogueDataSO.Choice>> OnEventRaised;

	public void RaiseEvent(List<DialogueDataSO.Choice> choices)
	{
		OnEventRaised?.Invoke(choices);
	}
}
