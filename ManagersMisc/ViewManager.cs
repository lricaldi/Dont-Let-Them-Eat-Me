using UnityEngine;
using System.Collections;

public class ViewManager : MonoBehaviour 
{
    public static ViewManager instance = null;
    
    private Transform m_bottomScreen;
    private Transform m_targetPos;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
            
		m_bottomScreen = GetComponent<Transform>().Find("bottomScreen");
		m_targetPos = GetComponent<Transform>().Find("targetPos");
    }

    public float getBottomScreenY()
    {
        return m_bottomScreen.position.y;
    }

    public Transform getTargetPos()
    {
        return m_targetPos;
    }
}
