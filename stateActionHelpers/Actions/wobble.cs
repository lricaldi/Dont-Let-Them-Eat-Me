using UnityEngine;
using System.Collections;

public class wobble : StateActionBase
{
    private float m_angle;
    private float m_speed;
    private float m_curTime;

    private Transform m_objTransform;
    private Vector3 m_rotation;

	public wobble(Transform objTransform, float angle, float speed)
	{
        setup(objTransform, angle, speed);
        m_rotation = Vector3.zero;
	}


    public void setup(Transform objTransform, float angle, float speed)
	{
        m_objTransform = objTransform;

        m_angle = angle;
        m_speed = speed;
        m_curTime = 0;
        
        m_done = false;
	}

    public override void update(float delta)
	{
        m_curTime += m_speed * delta;

        if (m_curTime > 1)
        {
            m_curTime = 1;
            m_speed = -m_speed;
        }
        else if (m_curTime < 0)
        {
            m_curTime = 0;
            m_speed = -m_speed; 
        }

        float newAngle = Mathf.Lerp(-m_angle, m_angle, m_curTime);
        m_rotation.z = newAngle;
        m_objTransform.rotation = Quaternion.Euler(m_rotation);

    }
}
