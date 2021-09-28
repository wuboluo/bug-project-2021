using UnityEngine;

namespace Bug.Project21.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Dialogue[] dialogue;

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}