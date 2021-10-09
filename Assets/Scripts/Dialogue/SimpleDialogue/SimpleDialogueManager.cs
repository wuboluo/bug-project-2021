using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Bug.Project21.Dialogues
{
    public class SimpleDialogueManager : MonoBehaviour
    {
        public Queue<SimpleDialogue> sentences;

        public TextMeshProUGUI nameText, diaText;

        [SerializeField] private SimpleDialogueSO simpleDialogue;

        public DialogueInputControl controls;

        private void Awake()
        {
            controls = new DialogueInputControl();
        }

        private void Start()
        {
            sentences = new Queue<SimpleDialogue>();

            controls.Dialogue.StartTalk.performed += _ => simpleDialogue.TriggerDialogue();
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

        public void StartDialogue(SimpleDialogue[] dialogue)
        {
            Debug.Log("start with " + dialogue.First().name + "  " + dialogue.First().sentence);

            sentences = new Queue<SimpleDialogue>(dialogue);

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

        /// <summary>
        ///     Complete all sentences.
        /// </summary>
        private static void EndDialogue()
        {
            print("completed dialogue");
        }
    }
}