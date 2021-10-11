using UnityEngine;

public class FollowPathState : FSMState
{
    private int currentWayPoint;
    private Transform[] waypoints;

    public FollowPathState(Transform[] wp)
    {
        waypoints = wp;
        currentWayPoint = 0;
        stateID = StateID.FollowingPath;
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        // If the Player passes less than 15 meters away in front of the NPC
        if (Physics.Raycast(npc.transform.position, npc.transform.forward, out var hit, 15F))
            if (hit.transform.gameObject.CompareTag("Player"))
                npc.GetComponent<NPCControl>().SetTransition(Transition.SawPlayer);
    }

    public override void Act(GameObject player, GameObject npc)
    {
        // Follow the path of waypoints
        // Find the direction of the current way point 
        var vel = npc.GetComponent<Rigidbody>().velocity;
        var moveDir = waypoints[currentWayPoint].position - npc.transform.position;

        if (moveDir.magnitude < 1)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length) currentWayPoint = 0;
        }
        else
        {
            vel = moveDir.normalized * 10;

            // Rotate towards the waypoint
            npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation,
                Quaternion.LookRotation(moveDir),
                5 * Time.deltaTime);
            npc.transform.eulerAngles = new Vector3(0, npc.transform.eulerAngles.y, 0);
        }

        // Apply the Velocity
        npc.GetComponent<Rigidbody>().velocity = vel;
    }
} // FollowPathState