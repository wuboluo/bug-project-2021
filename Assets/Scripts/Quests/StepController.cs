using UnityEngine;

public class StepController : MonoBehaviour
{
    [Header("Data")] 
    [SerializeField] private ActorSO actor;
    [SerializeField] private DialogueDataSO defaultDialogue;
    [SerializeField] private QuestManagerSO questMgr;

    [Header("Listening")] 
    [SerializeField] private VoidEventChannelSO _winDialogueEvent;
    [SerializeField] private VoidEventChannelSO _loseDialogueEvent;
    [SerializeField] private IntEventChannelSO _endDialogueEvent;

    [Header("Broadcasting")] 
    public DialogueDataChannelSO _startDialogueEvent;

    [Header("Dialogue UI")]
    public GameObject dialogueUI;

    private DialogueDataSO currentDialogue;

    private void Start()
    {
        if (!dialogueUI) return;

        // dialogueUI.transform.parent = null;
        dialogueUI.SetActive(false);
    }

    /// <summary>
    ///     播放默认对话。用于 NPC未设置开始对话内容
    /// </summary>
    private void PlayDefaultDialogue()
    {
        if (defaultDialogue == null) return;

        currentDialogue = defaultDialogue;
        StartDialogue();
    }

    /// <summary>
    ///     和玩家互动，若此 NPC未设置开始对话，则执行默认对话。否则执行开始对话
    /// </summary>
    public void InteractWithCharacter()
    {
        var displayDialogue = questMgr.DifferentDialoguesWithActor(actor, false, false);
        if (displayDialogue != null)
        {
            currentDialogue = displayDialogue;
            StartDialogue();
        }
        else
        {
            PlayDefaultDialogue();
        }
    }

    /// <summary>
    ///     开始对话。引发 开始对话事件，监听（结束对话、完成对话、未完成对话）事件。显示相关对话 UI
    /// </summary>
    private void StartDialogue()
    {
        _startDialogueEvent.RaiseEvent(currentDialogue);
        _endDialogueEvent.OnEventRaised += EndDialogue;
        _winDialogueEvent.OnEventRaised += PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised += PlayLoseDialogue;

        if (dialogueUI) dialogueUI.SetActive(true);
    }

    /// <summary>
    ///     结束对话。取消监听（结束对话、完成对话、未完成对话）事件，关闭相关对话 UI
    /// </summary>
    /// <param name="dialogueType"></param>
    private void EndDialogue(int dialogueType)
    {
        _endDialogueEvent.OnEventRaised -= EndDialogue;
        _winDialogueEvent.OnEventRaised -= PlayWinDialogue;
        _loseDialogueEvent.OnEventRaised -= PlayLoseDialogue;

        if (dialogueUI) dialogueUI.SetActive(false);
    }

    /// <summary>
    ///     播放成功对话（任务完成）
    /// </summary>
    private void PlayWinDialogue()
    {
        if (questMgr == null) return;
        var displayDialogue = questMgr.DifferentDialoguesWithActor(actor, true, true);
        if (displayDialogue != null)
        {
            currentDialogue = displayDialogue;
            StartDialogue();
        }
    }

    /// <summary>
    ///     播放失败对话（任务未完成）
    /// </summary>
    private void PlayLoseDialogue()
    {
        if (questMgr == null) return;
        var displayDialogue = questMgr.DifferentDialoguesWithActor(actor, true, false);
        if (displayDialogue != null)
        {
            currentDialogue = displayDialogue;
            StartDialogue();
        }
    }
}