using System.Collections;
using UnityEngine;

public enum NPCState
{
    Idle = 0,
    Walk,
    Talk
}

public class NPC : MonoBehaviour
{
    public NPCState npcState;
    public GameObject[] talkingTo;

    public void SwitchToWalkState()
    {
        StartCoroutine(WaitBeforeSwitch());
    }

    private IEnumerator WaitBeforeSwitch()
    {
        var wait_time = Random.Range(0, 4);
        yield return new WaitForSeconds(wait_time);
        npcState = NPCState.Walk;
    }
}