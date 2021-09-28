using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Bug.Project21.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;

        public Queue<Dialogue> sentences = new Queue<Dialogue>();

        public TextMeshProUGUI nameText, diaText;

        private DialogueTrigger diaTrigger;

        public DialogueInputControl controls;

        private void Awake()
        {
            instance = this;
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

        public void StartDialogue(Dialogue[] dialogue)
        {
            Debug.Log("start with " + dialogue.First().name + "  " + dialogue.First().sentence);

            sentences = new Queue<Dialogue>(dialogue);

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

            nameText.text = s.name;
            diaText.text = s.sentence;
            print($"{s.name} : {s.sentence}");
        }

        private static void EndDialogue()
        {
            print("dialogue finished");
        }
    }
}