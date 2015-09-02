using UnityEngine;
using System.Collections;

public class GroupActions : StateActionBase
{

	StateActionBase[] m_actions;
	private float m_timer;


	public override void reset()
	{
		base.reset();
		m_timer = 0;
		for (int i = 0; i < m_actions.Length; i++)
		{
			m_actions[i].reset();
		}
	}

	public GroupActions(StateActionBase[] actions)
	{
		m_actions = actions;
		reset();
	}
	public override void update(float delta) 
	{
        m_timer += delta;
		bool allDone = true;
		if (!m_done)
		{
			for (int i = 0; i < m_actions.Length; i++)
			{
				if (!m_actions[i].isDone())
				{
					allDone = false;
                    
					if (m_timer > m_actions[i].getStartTime())
					{
                        m_actions[i].update(delta);
					}
				}
			}
			m_done = allDone;
		}
	}
	

}
