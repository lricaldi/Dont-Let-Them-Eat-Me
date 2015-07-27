using UnityEngine;
using System.Collections;

public class StateBaseWithActions<T> : StateBase<T> 
{

    protected StateActionBase[] m_actions;
    protected int               m_curAction;

    public StateBaseWithActions(T refObj)
        : base(refObj)
    {
    }

    public override void runState()
    {
        m_actions[m_curAction].update();

        if (m_actions[m_curAction].isDone())
        {
            m_actions[m_curAction].reset();
            
            actionDone();
            m_curAction++;
        }
    }

    protected virtual void actionDone()
    {
        if (m_curAction+1 >= m_actions.Length)
        {
            curStep = StateStep.SSEnd;
        }
    }
}
