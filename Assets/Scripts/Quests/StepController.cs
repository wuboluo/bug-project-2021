using UnityEngine;

public class StepController : MonoBehaviour
{
    [Header("Data")] [SerializeField] private ActorSO _actor;

    [SerializeField] private DialogueDataSO _defaultDialogue;
    [SerializeField] private QuestManagerSO _questData;

    [Header("Listening to channels")] [SerializeField]
    private VoidEventChannelSO _winDialogueEvent;

    [SerializeField] private VoidEventChannelSO _loseDialogueEvent;
    [SerializeField] private IntEventChannelSO _endDialogueEvent;

    [Header("Broadcasting on channels")] public DialogueDataChannelSO _startDialogueEvent;

    [Header("Dialogue Shot Camera")] public GameObject dialogueShot;

    public bool isInDialogue; //Consumed by the state machine

    //check if character is actif. An actif character is the character concerned by the step.
    private DialogueDataSO _currentDialogue;

    private void Start()
    {
        if (!dialogueShot) return;

        dialogueShot.transform.parent = null;
        dialogueShot.SetActive(false);
    }

    private void PlayDefaultDialogue()
    {
        if (_defaultDialogue == null) return;

        _currentDialogue = _defaultDialogue;
        StartDialogue();
    }

    public void InteractWithCharacter()
    {
        var displayDialogue = _questData.InteractWithCharacter(_actor, false, false);
        if (displayDialogue != null)
        {
            _currentDialogue = displayDialogue;
            StartDialogue();
        }
        else
        {
            PlayDefaultDialogue();
        }
    }

    private void StartDialogue()
    {
        _startDialogueEvent.RaiseEvent(_currentDialogue);
        _endDialogueEvent.OnEventRaised += EndDialogue;
        StopConversation();
        _winDialogueEvent.OnEventRaised += PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised += PlayLoseDialogue;
        isInDialogue = true;
        if (dialogueShot)
            dialogueShot.SetActive(true);
    }

    private void EndDialogue(int dialogueType)
    {
        _endDialogueEvent.OnEventRaised -= EndDialogue;
        _winDialogueEvent.OnEventRaised -= PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised -= PlayLoseDialogue;
        ResumeConversation();
        isInDialogue = false;
        if (dialogueShot)
            dialogueShot.SetActive(false);
    }

    private void PlayLoseDialogue()
    {
        if (_questData == null) return;
        var displayDialogue = _questData.InteractWithCharacter(_actor, true, false);
        if (displayDialogue == null) return;

        _currentDialogue = displayDialogue;
        StartDialogue();
    }

    private void PlayWinDialogue()
    {
        if (_questData == null) return;
        var displayDialogue = _questData.InteractWithCharacter(_actor, true, true);
        if (displayDialogue == null) return;

        _currentDialogue = displayDialogue;
        StartDialogue();
    }

    private void StopConversation()
    {
        var talkingTo = gameObject.GetComponent<NPC>().talkingTo;
        if (talkingTo == null) return;

        foreach (var t in talkingTo) t.GetComponent<NPC>().npcState = NPCState.Idle;
    }

    private void ResumeConversation()
    {
        var talkingTo = GetComponent<NPC>().talkingTo;
        if (talkingTo == null) return;

        foreach (var t in talkingTo) t.GetComponent<NPC>().npcState = NPCState.Talk;
    }
}