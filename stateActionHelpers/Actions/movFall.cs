using UnityEngine;
using System.Collections;

public class movFall : StateActionBase
{

    private Vector2		m_fallSpeed;
    private float       m_yEndFall;
	private float       m_rotateSpeed; // on z axis
    private float       m_curRotation;
    private Vector2     m_startVel;
    private Vector2     m_curVel;
    private Transform   m_obj;
    private Transform   m_objRot;

    private Vector3     m_rotation;


	
    private bool m_started;

    public movFall()
    {
       // User needs to initialize it with the full contructor or setup before this action can be used.
    }

    public movFall(Transform obj, Vector2 startVel, float fallSpeed, float bottomY, float rotationSpeed, Transform objRot = null)
	{
        setup(obj, startVel, fallSpeed, bottomY, rotationSpeed, objRot);
        
	}


    public void setup(Transform obj, Vector2 startVel, float fallSpeed, float bottomY, float rotationSpeed, Transform objRot = null)
	{
		
		m_obj               = obj;
		m_fallSpeed         = Vector2.up * fallSpeed;
        m_yEndFall          = bottomY;
        m_rotateSpeed       = rotationSpeed;
        m_curRotation       = 0;
        m_startVel          = startVel;
        m_curVel            = m_startVel;
        m_rotation          = Vector3.zero;
        if (objRot == null)
        {
            m_objRot = m_obj;
        }
        else
        {
            m_objRot = objRot;
        }

        reset();
		
	}


    public override void update(float delta)
	{
        m_curRotation += (delta * m_rotateSpeed);
        m_rotation.z    = m_curRotation;

        m_objRot.rotation = Quaternion.Euler(m_rotation);

        
        Vector2 curPos = m_obj.position;
        m_curVel += m_fallSpeed * delta;
        //curPos += m_curVel;
        curPos += (m_curVel * delta);
        m_obj.position = curPos;

        if (curPos.y < m_yEndFall)
        {
            m_curVel    = m_startVel;
            m_rotation  = Vector3.zero;
            m_done      = true;
        }

       
    }
}
