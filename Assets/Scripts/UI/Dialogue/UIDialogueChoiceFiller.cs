using Bug.Project21.Dialogue;
using UnityEngine;

public class UIDialogueChoiceFiller : MonoBehaviour
{
	[Header("Broadcasting")]
	[SerializeField] private DialogueChoiceChannelSO _onChoiceMade;

	[SerializeField] private DialogueDataSO.Choice _currentChoice;
	
	public void ButtonClicked()
	{
		_onChoiceMade.RaiseEvent(_currentChoice);
	}
}
