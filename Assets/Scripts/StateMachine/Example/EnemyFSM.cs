using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public GameObject player;
    public Transform[] path;
    public float warningDistance;

    private FSMSystem fsm;

    public void Start()
    {
        MakeFSM();
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);
    }

    public void SetTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    private void MakeFSM()
    {
        var follow = new FollowPathState(path, this);
        follow.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);

        var chase = new ChasePlayerState(this);
        chase.AddTransition(Transition.LostPlayer, StateID.FollowingPath);

        fsm = new FSMSystem();
        fsm.AddState(follow);
        fsm.AddState(chase);
    }
}