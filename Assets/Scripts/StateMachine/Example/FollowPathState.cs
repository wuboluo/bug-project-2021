using UnityEngine;

public class FollowPathState : FSMState
{
    private int currentWayPoint;
    private Rigidbody2D rb;
    private Transform[] waypoints;

    public FollowPathState(Transform[] _wp, Rigidbody2D _rb)
    {
        waypoints = _wp;
        rb = _rb;
        currentWayPoint = 0;
        stateID = StateID.FollowingPath;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        if (Vector3.Distance(player.transform.position, npc.transform.position) < 5)
            npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
    }

    public override void Act(GameObject player, GameObject npc)
    {
        var vel = rb.velocity;
        var moveDir = waypoints[currentWayPoint].position - npc.transform.position;

        if (moveDir.magnitude < 0.2f)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length) currentWayPoint = 0;
        }
        else
        {
            vel = moveDir.normalized * 3;
        }
        rb.velocity = vel;
    }
}