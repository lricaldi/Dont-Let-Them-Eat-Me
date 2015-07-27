using UnityEngine;
using System.Collections;

public class StateActionBase  
{
	protected float     m_startTime = 0;
	protected bool      m_done;
		
	public bool isDone()        { return m_done;}
	public void forceDone()     { m_done = true; }
	public float getStartTime() { return m_startTime; }

    public virtual void forceEndAction() {m_done = true;}
    
	public virtual void update() {}
	public virtual void reset()  { m_done = false; }
	
}
