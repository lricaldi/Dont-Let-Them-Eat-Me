using UnityEngine;
using System.Collections;

public class ObjectWithStates<T> : MonoBehaviour 
{

    protected StateBase<T>[]    m_states;
    protected int               m_curState;
    protected float             m_updateTime;
    private float               m_timer;
    
	void Start () 
    {
        init();
        initStates();
        m_updateTime = 0.01f;
	}


    public void setUpdateTime(float updateSpeed)
    {
        m_updateTime = updateSpeed;
    }

    void Update()
    {
        m_timer += Time.deltaTime;
        if (m_timer >= m_updateTime)
        {
            update(m_timer);
            m_timer = 0;
        }
        
    }

    protected virtual void update(float delta)
    {
        m_states[m_curState].updateState(delta);
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
