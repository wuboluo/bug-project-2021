using UnityEngine;

public class UIDialogueChoiceFiller : MonoBehaviour
{
	[Header("Broadcasting on")]
	[SerializeField] private DialogueChoiceChannelSO _onChoiceMade;

	[SerializeField] private DialogueDataSO.Choice _currentChoice;
	
	public void ButtonClicked()
	{
		_onChoiceMade.RaiseEvent(_currentChoice);
	}
}
