using UnityEngine;

public class FollowPathState : FSMState
{
    private int currentWayPoint;

    private EnemyFSM fsm;
    private EnemyAI enemyAI;
    private Transform[] waypoints;

    public FollowPathState(Transform[] _wp, EnemyFSM _fsm)
    {
        waypoints = _wp;
        fsm = _fsm;

        currentWayPoint = 0;
        stateID = StateID.FollowingPath;
        enemyAI = fsm.GetComponent<EnemyAI>();
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        var dis = Vector3.Distance(player.transform.position, npc.transform.position);
        if (dis < fsm.warningDistance)
            npc.GetComponent<EnemyFSM>().SetTransition(Transition.SawPlayer);
    }

    public override void Act(GameObject player, GameObject npc)
    {
        var moveDir = waypoints[currentWayPoint].position - npc.transform.position;

        if (moveDir.magnitude < 1.5f)
        {
            currentWayPoint++;
            if (currentWayPoint >= waypoints.Length) currentWayPoint = 0;
            enemyAI.target = waypoints[currentWayPoint];
        }
    }

    public override void DoBeforeEntering()
    {
        enemyAI.target = waypoints[currentWayPoint];
    }
}