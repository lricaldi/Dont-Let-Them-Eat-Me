using UnityEngine;
using System.Collections;

public class movOscilate : StateActionBase
{

    private GameObject m_obj;

    private Vector2 startPos;
    private Vector2 newPos;

	private float m_horzOcillation;
    private float m_vertOcillation;

    private float m_horzSpeed;
    private float m_vertSpeed;


    private float m_horzTime;
    private float m_vertTime;

    private bool m_started;
	public override void reset()
	{
		base.reset();
        m_started = false;
	}

    public movOscilate()
    {
    }

    public movOscilate(GameObject obj, float horzOcill, float vertOcill, float horzSpeed, float vertSpeed)
	{
        setup(obj, horzOcill, vertOcill, horzSpeed, vertSpeed);
	}

    public void setup(GameObject obj, float horzOcill, float vertOcill, float horzSpeed, float vertSpeed)
	{
        m_horzOcillation = horzOcill;
        m_vertOcillation = vertOcill;
        m_horzSpeed = horzSpeed;
        m_vertSpeed = vertSpeed;

        m_horzTime = 0.5f;
        m_vertTime = 0.5f;

        m_obj = obj;

        newPos = new Vector2();

        reset();
	}

    public void finishAction()
    {
        m_done = true;
    }

    public override void update(float delta)
	{

       
        if (m_done) return;
        if (!m_started)
        {
            startPos = m_obj.GetComponent<Transform>().position;
            m_started = true;
        }

        newPos = m_obj.GetComponent<Transform>().position;

        if( m_horzOcillation != 0)
        {

            m_horzTime += m_horzSpeed * delta;
            if (m_horzTime > 1)
            {
                m_horzTime = 1;
                m_horzSpeed = -m_horzSpeed;
            }
            else if (m_horzTime < 0)
            {
                m_horzTime = 0;
                m_horzSpeed = -m_horzSpeed;
            }
            
            newPos.x = Mathf.Lerp(startPos.x - m_horzOcillation, startPos.x + m_horzOcillation, m_horzTime);
        }

        if(m_vertOcillation != 0)
        {

            m_vertTime += m_vertSpeed * delta;

            if (m_vertTime > 1)
            {
                m_vertTime = 1;
                m_vertSpeed = -m_vertSpeed;
            }
            else if (m_vertTime < 0)
            {
                m_vertTime = 0;
                m_vertSpeed = -m_vertSpeed;
            }

            newPos.y = Mathf.Lerp(startPos.y - m_vertOcillation, startPos.y + m_vertOcillation, m_vertTime);
        }
        m_obj.GetComponent<Transform>().position = newPos;
	
	}
}
