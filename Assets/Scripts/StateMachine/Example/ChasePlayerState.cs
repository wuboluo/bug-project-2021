using UnityEngine;

public class ChasePlayerState : FSMState
{
    private Rigidbody2D rb;

    public ChasePlayerState(Rigidbody2D _rb)
    {
        stateID = StateID.ChasingPlayer;
        rb = _rb;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Vector3.Distance(npc.transform.position, player.transform.position) >= 5)
            npc.GetComponent<NPCControl>().SetTransition(Transition.LostPlayer);
    }

    public override void Act(GameObject player, GameObject npc)
    {
        var moveDir = player.transform.position - npc.transform.position;
        var vel = moveDir.normalized * 3;

        rb.velocity = vel;
    }
}