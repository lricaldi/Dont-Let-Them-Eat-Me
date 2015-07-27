using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTarget : MonoBehaviour 
{
    private const int MAX_HEALTH    = 100;
    public const int OK_HEALTH      = 50;
    public const int SAD_HEALTH     = 25;
    public const int MAX_ATTACH     = 6;

    public enum StateEnum { SE_HAPPY, SE_OK, SE_SAD, SE_EATEN, SE_ATTACKED, SE_WIN, SE_Length }

    private int                 m_health;
    private GameObject          m_targetView;
    public int                  m_numAttachedEnemies;
    StateBase<EnemyTarget>[]    m_states;
    private int                 m_curState;
    private List<Enemy>         m_enemiesAttached;
    private wobble              m_wobbleAction;
	

    private void sceneEvent(SceneManager.SceneEvent sceneEvent, int valueOne)
    {
        switch (sceneEvent)
        {
            case SceneManager.SceneEvent.SE_TARGETWON:
               
                m_states[m_curState].resetState();
                m_curState = (int)StateEnum.SE_WIN;
                break;
        }

    }

	void Start ()
    {
        m_targetView            = GetComponent<Transform>().Find("treasureView").gameObject;

        m_enemiesAttached       = new List<Enemy>(EnemyTarget.MAX_ATTACH);
	
        m_curState              = (int)StateEnum.SE_HAPPY;
        m_health                = MAX_HEALTH;
        m_numAttachedEnemies    = 0;

        initStates();

        SceneManager.instance.registerForSceneEvent(new SceneManager.sceneEventHandler(sceneEvent));
	}



    public void addToHealth(int value)
    {
        m_health += value;
        m_health = (m_health < 0)? 0: m_health;
        m_health = (m_health > MAX_HEALTH)? MAX_HEALTH: m_health;

        if (m_health <= 0)
        {
            SceneManager.instance.targetDied();
            m_states[m_curState].resetState();
            m_curState = (int)StateEnum.SE_EATEN;
        }
        
        SceneManager.instance.targetHealthUpdate(m_health);
        

    }
    
    public int getHealth()
    {
        return m_health;
    }

    public Animator getViewAnimator()
    {
        return m_targetView.GetComponent<Animator>();
    }

    public int getNumAttachedEnemies()
    {
        return m_numAttachedEnemies;
    }
    
    public void setCurrentState(StateEnum newState)
    {
        m_curState = (int)newState;
    }
    
    public Vector2 attachEnemy(Enemy enemy)
    {
        if (m_numAttachedEnemies >= MAX_ATTACH)
        {
            return Vector2.zero;
        }
        m_numAttachedEnemies++;
        m_enemiesAttached.Add(enemy);
        Vector2 randomPoint = Random.insideUnitCircle;
        randomPoint         *= (m_targetView.GetComponent<SpriteRenderer>().bounds.size.x/2);
        return (Vector2)GetComponent<Transform>().position + randomPoint;
    }

    public void removeEnemy()
    {
        m_numAttachedEnemies--;
        m_numAttachedEnemies = (m_numAttachedEnemies < 0)?0: m_numAttachedEnemies;
       
    }

    public bool canAttachEnemy()
    {
        return m_numAttachedEnemies < MAX_ATTACH;
    }

	void Update () 
    {
        m_states[m_curState].updateState();
	}

    private void initStates()
    {
        m_states = new StateBase<EnemyTarget>[(int)StateEnum.SE_Length];

        m_states[(int)StateEnum.SE_HAPPY]       = new TargetHappy(this);
        m_states[(int)StateEnum.SE_OK]          = new TargetOk(this);
        m_states[(int)StateEnum.SE_SAD]         = new TargetSad(this);
        m_states[(int)StateEnum.SE_EATEN]       = new TargetEaten(this);
        m_states[(int)StateEnum.SE_ATTACKED]    = new TargetAttacked(this);
        m_states[(int)StateEnum.SE_WIN]         = new TargetWin(this);
    }

    public Transform getViewTransform()
    {
        return m_targetView.GetComponent<Transform>();
    }

    public void killAttached()
    {
        foreach (Enemy curEnemy in m_enemiesAttached)
        {
            curEnemy.kill();
        }
        m_enemiesAttached.Clear();

        m_numAttachedEnemies = 0;
        
    }
}
