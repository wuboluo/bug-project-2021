using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Bug.Project21.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        public Queue<string> sentences = new Queue<string>();

        public TextMeshProUGUI nameText, diaText;

        private DialogueTrigger diaTrigger;

        public DialogueInputControl controls;

        private void Awake()
        {
            controls = new DialogueInputControl();
        }

        private void Start()
        {
            diaTrigger = GetComponent<DialogueTrigger>();
            
            controls.Dialogue.StartTalk.performed += _ => diaTrigger.TriggerDialogue();
            controls.Dialogue.Next.performed += _ => DisplayNextSentence();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Debug.Log("start with " + dialogue.name);

            nameText.text = dialogue.name;
            sentences.Clear();

            foreach (var s in dialogue.sentences) sentences.Enqueue(s);

            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (sentences.Count == 0)
            {
                EndDialogue();
                return;
            }

            var s = sentences.Dequeue();
            diaText.text = s;
            print(s);
        }

        private static void EndDialogue()
        {
            print("dialogue finished");
        }
    }
}