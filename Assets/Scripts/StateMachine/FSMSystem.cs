using System.Collections.Generic;
using System.Linq;

public class FSMSystem
{
    // 通过转换改变 FSM的状态，而不是直接改变 CurrentState
    private List<FSMState> states;

    public FSMSystem()
    {
        states = new List<FSMState>();
    }

    public StateID CurrentStateID { get; private set; }

    public FSMState CurrentState { get; private set; }

    public void AddState(FSMState s)
    {
        if (s == null) return;

        // 插入的第一个状态即初始状态
        if (states.Count == 0)
        {
            states.Add(s);
            CurrentState = s;
            CurrentStateID = s.ID;
            return;
        }

        if (states.Any(state => state.ID == s.ID))
            return;

        states.Add(s);
    }

    public void DeleteState(StateID id)
    {
        if (id == StateID.NullStateID) return;

        foreach (var state in states.Where(state => state.ID == id))
        {
            states.Remove(state);
            return;
        }
    }

    /// <summary>
    ///     执行转换
    /// </summary>
    public void PerformTransition(Transition trans)
    {
        if (trans == Transition.NullTransition) return;

        var id = CurrentState.GetOutputState(trans);
        if (id == StateID.NullStateID) return;

        // 更新 currentStateID 和 currentState       
        CurrentStateID = id;
        foreach (var state in states.Where(state => state.ID == CurrentStateID))
        {
            // 在设置新状态之前做状态的后处理
            CurrentState.DoBeforeLeaving();

            CurrentState = state;

            // 在它可以推理或采取行动之前将状态重置为其所需的条件
            CurrentState.DoBeforeEntering();
            break;
        }
    }
}