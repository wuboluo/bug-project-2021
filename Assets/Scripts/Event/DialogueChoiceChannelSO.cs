using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Bug/Events/UI/Dialogue Choice Channel")]
public class DialogueChoiceChannelSO : ScriptableObject
{
	public UnityAction<DialogueDataSO.Choice> OnEventRaised;
	public void RaiseEvent(DialogueDataSO.Choice choice)
	{
		OnEventRaised?.Invoke(choice);
	}
}
