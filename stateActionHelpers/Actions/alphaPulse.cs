using UnityEngine;
using System.Collections;

public class alphaPulse : StateActionBase
{
	private GameObject	m_obj;

    
    private float m_speed;
    private float m_acceleration;
    private bool m_fadingOut;
    private bool m_startedFadingOut;
    
    private float m_alphaValue;

    private float m_curSpeed;

    private float m_maxSpeed;
    private float m_minAlpha;
    private float m_maxAlpha;

    private int m_numPulses;
    private int m_pulseCount;

    private Color m_color;

    public alphaPulse(){}

    public alphaPulse(GameObject obj, float speed, float maxSpeed, float acceleration, float minAlpha, float maxAlpha, int numPulses = 0, bool startFadeOut = true)
    {
        setup(obj, speed, maxSpeed, acceleration, minAlpha, maxAlpha, numPulses, startFadeOut);

    }

    public void setup(GameObject obj, float speed, float maxSpeed, float acceleration, float minAlpha, float maxAlpha, int numPulses = 0, bool startFadeOut = true)
	{
	    m_obj = obj;
        m_speed = speed;
        m_maxSpeed = maxSpeed;
        m_acceleration = acceleration;
        m_fadingOut = startFadeOut;
        m_startedFadingOut = startFadeOut;
        m_color = m_obj.GetComponent<SpriteRenderer>().color;
        m_alphaValue = m_color.a;
        m_maxAlpha = maxAlpha;
        m_minAlpha = minAlpha;
        m_numPulses = numPulses;
        m_pulseCount = 0;
        m_done = false;
	}

	
	public override void update()
	{
        if (m_fadingOut)
        {
            m_alphaValue -= m_speed * Time.deltaTime;
            if (m_alphaValue <= m_minAlpha)
            {
                m_alphaValue = m_minAlpha;
                m_fadingOut = false;
                if (!m_startedFadingOut)
                {
                    m_pulseCount++;
                }
            }
        }
        else
        {
            m_alphaValue += m_speed * Time.deltaTime;
            if (m_alphaValue >= m_maxAlpha)
            {
                m_alphaValue = m_maxAlpha;
                m_fadingOut = true;
                if (m_startedFadingOut)
                {
                    m_pulseCount++;
                }
            }
        }

      
        m_speed += m_acceleration * Time.deltaTime;
        if (m_speed > m_maxSpeed) { m_speed =  m_maxSpeed;}
        if (m_speed < 0.02f) { m_speed = 0.02f; }

        m_color.a = m_alphaValue;
        m_obj.GetComponent<SpriteRenderer>().color = m_color;

        if (m_numPulses > 0 && m_pulseCount >= m_numPulses)
        {
            m_done = true;
        }

      
	}

}
