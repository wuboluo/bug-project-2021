using UnityEngine;

public class ChasePlayerState : FSMState
{
    private EnemyFSM fsm;
    private EnemyAI enemyAI;
    
    public ChasePlayerState( EnemyFSM _fsm)
    {
        stateID = StateID.ChasingPlayer;
        fsm = _fsm;
        enemyAI = fsm.GetComponent<EnemyAI>();
    }

    public override void Reason(GameObject player, GameObject npc)
    {
        var dis = Vector3.Distance(npc.transform.position, player.transform.position);
        if (dis >= fsm.warningDistance)
            npc.GetComponent<EnemyFSM>().SetTransition(Transition.LostPlayer);
    }

    public override void Act(GameObject player, GameObject npc)
    {
       
    }
    
    public override void DoBeforeEntering()
    {
        enemyAI.target = fsm.GetComponent<Enemy>().player;
    }
}