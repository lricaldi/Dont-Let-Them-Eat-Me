using UnityEngine;
using System.Collections;

public class ObjectWithStates<T> : MonoBehaviour 
{

    protected StateBase<T>[]    m_states;
    protected int               m_curState;

	void Start () 
    {
        init();
        initStates();
	}

    void Update()
    {
        update();
    }

    protected virtual void update()
    {
        m_states[m_curState].updateState();
    }

    protected virtual void init()
    {
    }

    protected virtual void initStates()
    {
    }

    public virtual void setCurState(int newState)
    {
        m_curState = newState;
    }
}
