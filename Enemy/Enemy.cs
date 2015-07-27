using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
    public enum StateEnum { SE_IDLE, SE_MOVEUP, SE_EAT, SE_DEAD, SE_Length }

    private StateBase<Enemy>[]  m_states;
    private int                 m_curState;
    private pathNode            m_node;
    private EnemyView           m_enemyView;
    private bool                m_ready;
    
    public void reset()
    {
        m_node      = null;
        m_enemyView = null;
        m_ready     = false;
    }

    public void setNodeAndView(pathNode node, EnemyView enemyView)
    {
        m_node          = node;
        m_node.m_inUse  = true;
        m_enemyView     = enemyView;
    }

    public Animator getViewAnimator()
    {
        return m_enemyView.gameObject.GetComponent<Animator>();
    }

    public void setupStates()
    {
        m_states                            = new StateBase<Enemy>[(int)StateEnum.SE_Length];

        m_states[(int)StateEnum.SE_IDLE]    = new EnemyIdleState(this);
        m_states[(int)StateEnum.SE_MOVEUP]  = new EnemyMoveState(this);
        m_states[(int)StateEnum.SE_EAT]     = new EnemyEatingState(this);
        m_states[(int)StateEnum.SE_DEAD]    = new EnemyDeadState(this);

        m_curState                          = (int)StateEnum.SE_IDLE;

    }

    public void setReady(bool isReady)
    {
        m_ready = isReady;
    }

    public void setCurrentState(StateEnum newState)
    {
        m_curState = (int)newState;
    }

    public Vector2 getCurNodePos()
    {
        return m_node.m_nodeTransform.position;
    }

    public Vector2 getNextNodePos()
    {
        if (m_node.m_nextNode != null)
        {
            return m_node.m_nextNode.m_nodeTransform.position;
        }
        return Vector2.zero;
    }

    public bool isCurNodeEndNode()
    {
        return m_node.m_nextNode == null;
    }

    public bool isFastMoveNode()
    {
        return m_node.m_fastMove;
    }
    
    public void moveToNextNode()
    {
        m_node.m_inUse  = false;
        m_node          = m_node.m_nextNode;
        m_node.m_inUse  = true;
    }

    public bool nextNodeAvailable()
    {
        return !m_node.m_nextNode.m_inUse;
    }

	void Update () 
    {
        if(m_ready)
        {
        	if(GetComponent<Transform>().position.y < ViewManager.instance.getBottomScreenY() - 5)
        	{
        		kill();
        	}
            m_states[m_curState].updateState();
        }
	}

    public bool isOnPath()
    {
        return m_node != null;
    }

    public void freeNode()
    {
        if (m_node != null)
        {
            m_node.m_inUse  = false;
            m_node          = null;
        }
    }

    public EnemyView getView()
    {
        return m_enemyView;
    }

    public void cleanUp()
    {
        GetComponent<Transform>().localScale            = Vector3.one;
        
        m_enemyView.GetComponent<Transform>().rotation  = Quaternion.Euler(Vector3.zero);
        m_enemyView.gameObject.GetComponent<Transform>().SetParent(null);
        InstanceFactory.instance.freeEnemyView(m_enemyView);
        m_enemyView = null;
        
        InstanceFactory.instance.freeEnemy(this);
        SceneManager.instance.getEnemyManager().enemyKilled();
    }

    public void kill()
    {
        m_states[m_curState].resetState();
        m_curState = (int)StateEnum.SE_DEAD;
    }
}
