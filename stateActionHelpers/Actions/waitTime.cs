﻿using UnityEngine;
using System.Collections;

public class waitTime : StateActionBase 
{
	private float	m_timeToWait;
	private float 	m_curWaitTime;

	public override void reset()
	{
		base.reset();
		m_curWaitTime = 0;
	}

	public waitTime(float timeToWait)
	{
		setup(timeToWait);
	}

	public void setup(float timeToWait)
	{
		
		m_timeToWait = timeToWait;
		m_curWaitTime = 0;
		reset();
	}

    public override void update(float delta)
	{
        if (!m_done && m_timeToWait > 0) // if we pass 0 it will never stop waiting
		{
            m_curWaitTime += delta;
			m_done = m_curWaitTime > m_timeToWait;
		}
	
	}
}
