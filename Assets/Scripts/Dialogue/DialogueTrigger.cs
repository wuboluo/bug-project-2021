using UnityEngine;

namespace Bug.Project21.Dialogue
{
	public class DialogueTrigger : MonoBehaviour
	{
		[SerializeField] private DialogueManager dialogueManager;
		[SerializeField] private DialogueDataSO dialogueData;

		private void OnTriggerEnter2D(Collider2D other)
		{
			dialogueManager.DisplayDialogueData(dialogueData);
		}
	}
}