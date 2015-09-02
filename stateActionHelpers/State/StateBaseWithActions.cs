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

    public override void runState(float delta)
    {
        m_actions[m_curAction].update(delta);

        if (m_actions[m_curAction].isDone())
        {
            m_actions[m_curAction].reset();
            
            if(actionDone())
                m_curAction++;
        }
    }

    protected virtual bool actionDone()
    {
        if (m_curAction+1 >= m_actions.Length)
        {
            curStep = StateStep.SSEnd;
        }
        return true;
    }
}
