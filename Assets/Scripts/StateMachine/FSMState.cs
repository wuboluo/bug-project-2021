using System.Collections.Generic;
using UnityEngine;

public enum Transition
{
    NullTransition = 0,

    SawPlayer,
    LostPlayer
}

public enum StateID
{
    NullStateID = 0,

    ChasingPlayer,
    FollowingPath
}


public abstract class FSMState
{
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();
    protected StateID stateID;
    public StateID ID => stateID;

    public void AddTransition(Transition trans, StateID id)
    {
        if (trans == Transition.NullTransition) return;
        if (id == StateID.NullStateID) return;

        if (map.ContainsKey(trans)) return;

        map.Add(trans, id);
    }

    public void DeleteTransition(Transition trans)
    {
        if (trans == Transition.NullTransition) return;

        if (map.ContainsKey(trans)) map.Remove(trans);
    }

    public StateID GetOutputState(Transition trans)
    {
        return map.ContainsKey(trans) ? map[trans] : StateID.NullStateID;
    }

    public virtual void DoBeforeEntering()
    {
    }

    public virtual void DoBeforeLeaving()
    {
    }


    /// <summary>
    ///     用于确定应该触发哪个转换
    /// </summary>
    public abstract void Reason(GameObject player, GameObject npc);

    /// <summary>
    ///     NPC在此状态下应该执行的操作的代码
    /// </summary>
    public abstract void Act(GameObject player, GameObject npc);
}