using Bug.Project21.Dialogue;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Event/Dialogue/Dialogue Choice Channel")]
public class DialogueChoiceChannelSO : ScriptableObject
{
	public UnityAction<DialogueDataSO.Choice> OnEventRaised;
	public void RaiseEvent(DialogueDataSO.Choice choice)
	{
		OnEventRaised?.Invoke(choice);
	}
}
