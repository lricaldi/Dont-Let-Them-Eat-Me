using UnityEngine;
using System.Collections;

public class movAtoB : StateActionBase
{
	private Vector2 	m_startPos;
	private Vector2		m_endPos;
	private float		m_movSpeed;
	private GameObject	m_obj;
	private Vector2		m_direction;
    private float       m_curTime;

    private bool m_started;

	public movAtoB(){}

	public movAtoB(GameObject obj, Vector2 start,  Vector2 end, float speed, float startTime)
	{
		setup(obj, start,  end, speed, startTime);
	}


	public void setup(GameObject obj, Vector2 start, Vector2 end, float speed, float startTime)
	{
		m_startPos = start;
		m_endPos = end;
		m_obj = obj;
		m_movSpeed = speed;

		m_startTime = startTime;
        m_direction = (m_endPos - m_startPos).normalized;
		
        m_done = false;
        m_started = false;
		
		
	}


    public override void update(float delta)
	{
        if(!m_started)
        {
            m_obj.GetComponent<Transform>().position = m_startPos;
            m_curTime = 0;
            m_started = true;
            
        }
       
		else if(!m_done)
		{

            m_curTime += m_movSpeed * delta;


            if (m_curTime >= 1f)
            {
                m_curTime = 1;
                m_done = true;
            }

            m_obj.GetComponent<Transform>().position = Vector2.Lerp(m_startPos, m_endPos, m_curTime);
            
		}
    }
    	
}
