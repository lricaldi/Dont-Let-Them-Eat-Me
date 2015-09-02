using UnityEngine;
using System.Collections;

public class TargetCrumb : MonoBehaviour 
{
    private const float  LIFE_TIME = 0.5f;

    private float       m_timer;
    private movFall     m_fallAction;
    private bool        m_isTriggered;
    private Transform  m_view = null;


	void Start () 
    {
       
	}

    public void setup()
    {
        if (m_view == null)
        {
            m_view = GetComponent<Transform>().Find("bite");
        }
        float newScale = Random.Range(0.5f, 1);
        GetComponent<Transform>().localScale = new Vector3(newScale, newScale, 1);
        Vector2 startSpeed = new Vector2(Random.Range(-0.25f, 0.25f), 1f).normalized * 8;
        m_fallAction = new movFall(m_view, startSpeed, -40f, ViewManager.instance.getBottomScreenY() - 1, Random.Range(100, 150), m_view);


        //GetComponent<Transform>().position = pos;
        m_timer = 0;
        m_view.gameObject.SetActive(true);
        m_isTriggered = true;
    }


	// Update is called once per frame
	void Update () 
    {
        if (m_isTriggered)
        {
            m_fallAction.update(Time.deltaTime);

            m_timer += Time.deltaTime;

            if (m_timer > LIFE_TIME)
            {
                m_view.gameObject.SetActive(false);
                m_fallAction.forceDone();
                m_isTriggered = false;
                GetComponent<Transform>().localScale = Vector3.one;
                InstanceFactory.instance.freeTargetCrumb(this);
            }
        }
    }
}
