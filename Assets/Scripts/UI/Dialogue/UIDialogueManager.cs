using Bug.Project21.Dialogue;
using TMPro;
using UnityEngine;

public class UIDialogueManager : MonoBehaviour
{
    public DialogueLineChannelSO _openUIDialogueEvent;
    public VoidEventChannelSO _closeUIDialogueEvent;
    public VoidEventChannelSO _showChoiceEvent;
    public VoidEventChannelSO _hideChoiceEvent;

    // 临时写在对话 manager里
    public StringEventChannelSO _setQuestTargetEvent;
    
    public GameObject dialogueWindow;
    public GameObject choices;
    public TextMeshProUGUI talker;
    public TextMeshProUGUI sentence;

    public TextMeshProUGUI questTargetTxt;

    private void Start()
    {
        _openUIDialogueEvent.OnEventRaised += OpenDialogueWindowAndDisplayDialogue;
        _closeUIDialogueEvent.OnEventRaised += CloseDialogueWindow;
        _showChoiceEvent.OnEventRaised += ShowChoices;
        _hideChoiceEvent.OnEventRaised += HideChoices;

        _setQuestTargetEvent.OnEventRaised += SetQuestTargetOnUI;
    }

    private void OnDisable()
    {
        _openUIDialogueEvent.OnEventRaised -= OpenDialogueWindowAndDisplayDialogue;
        _closeUIDialogueEvent.OnEventRaised -= CloseDialogueWindow;
        _showChoiceEvent.OnEventRaised -= ShowChoices;
        _hideChoiceEvent.OnEventRaised -= HideChoices;
        
        _setQuestTargetEvent.OnEventRaised -= SetQuestTargetOnUI;
    }

    void OpenDialogueWindowAndDisplayDialogue(string _dialogueLine, ActorSO _actor)
    {
        ShowWindow();
        
        talker.text = _actor == null ? "Me" : _actor.name;
        sentence.text = _dialogueLine;
    }

    void ShowWindow() => dialogueWindow.SetActive(true);
    void CloseDialogueWindow() =>dialogueWindow.SetActive(false);

    void ShowChoices() => choices.SetActive(true);
    void HideChoices() => choices.SetActive(false);


    void SetQuestTargetOnUI(string str) => questTargetTxt.text = str;
}