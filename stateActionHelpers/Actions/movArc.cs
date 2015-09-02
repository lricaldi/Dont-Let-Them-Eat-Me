using UnityEngine;
using System.Collections;

public class movArc : StateActionBase
{
	private Vector2 	m_startPos;
	private Vector2		m_endPos;
	private float		m_movSpeed;
	private GameObject	m_obj;
	private Vector2		m_direction;
    private float       m_curTime;
    private float       m_downSpeed;
    private Vector2     m_velocity;

    private bool m_started;

    public movArc()
    {
        // This should not be used before running the action.
        setup(null, Vector2.zero, Vector2.zero, 0, 0, 0);
    }

    public movArc(GameObject obj, Vector2 start, Vector2 end, float speed, float startTime, float downSpeed)
	{
        setup(obj, start, end, speed, startTime, downSpeed);
	}


	public void setup(GameObject obj, Vector2 start, Vector2 end, float speed, float startTime, float downSpeed)
	{
        m_downSpeed = downSpeed;
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

            m_velocity = m_direction * m_movSpeed;
            m_curTime = 0;
            m_started = true;
            
        }
       
		else if(!m_done)
		{
            m_velocity.y += m_downSpeed;
         
            Vector2 curPos = m_obj.GetComponent<Transform>().position;

            curPos += (m_velocity * delta);
            m_obj.GetComponent<Transform>().position = curPos;
            
		}
    }
    	
}
