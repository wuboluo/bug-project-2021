using Bug.Project21.Dialogues;
using UnityEngine;

[CreateAssetMenu(fileName = "new SimpleDialogue", menuName = "Bug/Dialogue/SimpleDialogue")]
public class SimpleDialogueSO : ScriptableObject
{
    public SimpleDialogue[] dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<SimpleDialogueManager>().StartDialogue(dialogue);
    }
}