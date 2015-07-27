using UnityEngine;
using System.Collections;

public class movDir : StateActionBase
{

	private GameObject m_obj;
	private Vector2 m_direction;
	private float m_speed;
		
	public void setup(GameObject obj, Vector2 direction, float speed)
	{
		m_direction = direction.normalized;
		
		m_obj = obj;
		m_speed = speed;
		m_done = false;
		
	}

	public override void update()
	{
		if (!m_done)
		{
			Vector2 curPos = m_obj.GetComponent<Transform>().position;
			curPos += m_direction * m_speed * Time.deltaTime;
			m_obj.GetComponent<Transform>().position = curPos;
		}
	}

	public void release()
	{
		m_obj = null;
	}
}
