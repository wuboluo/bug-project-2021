using UnityEngine;

public class NPCControl : MonoBehaviour
{
    public GameObject player;
    public Transform[] path;
    private FSMSystem fsm;
    private Rigidbody2D rb;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        MakeFSM();
    }

    public void FixedUpdate()
    {
        fsm.CurrentState.Reason(player, gameObject);
        fsm.CurrentState.Act(player, gameObject);

        transform.localScale = rb.velocity.x > 0 ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    public void SetTransition(Transition t)
    {
        fsm.PerformTransition(t);
    }

    private void MakeFSM()
    {
        var follow = new FollowPathState(path, rb);
        follow.AddTransition(Transition.SawPlayer, StateID.ChasingPlayer);

        var chase = new ChasePlayerState(rb);
        chase.AddTransition(Transition.LostPlayer, StateID.FollowingPath);

        fsm = new FSMSystem();
        fsm.AddState(follow);
        fsm.AddState(chase);
    }
}