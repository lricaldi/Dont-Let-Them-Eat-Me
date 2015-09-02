using UnityEngine;
using System.Collections;

public class titleScreenEnemy : MonoBehaviour {

    private const int NUM_START_POS     = 7;
    private const float REMOVE_Y_POS    = -8;

    private Transform[] m_startPos;
    private Transform m_targetPos;
    
    private Transform m_enemyView;


    private movFall m_fallAction;
    private bool    m_isActive;
    private float m_nextStartTime;





	// Use this for initialization
	void Start () 
    {
        m_startPos = new Transform[NUM_START_POS];
        for(int i =0; i<NUM_START_POS; i++)
        {
            m_startPos[i] = GameObject.Find("pos"+i).GetComponent<Transform>();
        }
        m_targetPos = GameObject.Find("target").GetComponent<Transform>();

        m_enemyView = this.GetComponent<Transform>().Find("enemyView");

        
        m_isActive  = false;
        m_nextStartTime = Random.Range(0f, 3f);
        m_fallAction = new movFall();
        m_enemyView.rotation = Quaternion.identity;
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (m_isActive)
        {
        
                    
            if (!m_fallAction.isDone())
            {
                m_fallAction.update(Time.deltaTime);
            }
            else
            {
                m_isActive = false;
            }
        }
        else if (m_nextStartTime < 0)
        {
            m_isActive = true;
            int startPosIndex = (int)Mathf.Floor(Random.Range(0, m_startPos.Length));

            this.GetComponent<Transform>().position = m_startPos[startPosIndex].position;
            Vector2 startSpeed = (m_targetPos.position - m_startPos[startPosIndex].position).normalized * Random.Range(8, 10);
            m_fallAction.setup(this.GetComponent<Transform>(), startSpeed,
                                -6f, REMOVE_Y_POS,
                                Random.Range(-100, 100), m_enemyView);
            m_nextStartTime = Random.Range(0f, 3f);
        }
        else
        {
            m_nextStartTime -= Time.deltaTime;
        }
            


        
        
	}
}
