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
    private FXBase              m_effect;

    private BeltItem.EffectTypeEnum m_effectType;
    
    public void reset()
    {
        m_node      = null;
        m_enemyView = null;
        m_ready     = false;
    }

    public void setNodeAndView(pathNode node, EnemyView enemyView)
    {
        m_node          = node;
        //m_node.m_inUse  = true;
        node.m_refEnemy = this;
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
        //m_node.m_inUse      = false;
        m_node.m_refEnemy   = null;
        m_node              = m_node.m_nextNode;
        //m_node.m_inUse      = true;
        m_node.m_refEnemy   = this;
    }

    public bool nextNodeAvailable()
    {
        return m_node.m_nextNode.m_refEnemy == null;
       // return !m_node.m_nextNode.m_inUse;
    }

	void Update () 
    {
        if(m_ready)
        {
        	if(GetComponent<Transform>().position.y < ViewManager.instance.getBottomScreenY() - 5)
        	{
        		kill(BeltItem.EffectTypeEnum.ETE_NORMAL);
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
            //m_node.m_inUse      = false;
            m_node.m_refEnemy   = null;
            m_node              = null;
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
        if (m_effect != null)
        {
            m_effect.endEffect();
            m_effect = null;
        }
        m_effectType = BeltItem.EffectTypeEnum.ETE_NORMAL;
        
        InstanceFactory.instance.freeEnemy(this);
        SceneManager.instance.getEnemyManager().enemyKilled();
    }

    public bool doesEffectRotate()
    {
        return (m_effect != null && (m_effectType == BeltItem.EffectTypeEnum.ETE_ICE));
    }


    public void kill(BeltItem.EffectTypeEnum effectType, bool propagate = false)
    {
        m_effect = null;
        m_effectType = effectType;
        switch(effectType)
        {
            case BeltItem.EffectTypeEnum.ETE_FIRE:
                m_effect = EffectsManager.instance.getEffect(Vector2.zero, Quaternion.identity, EffectsManager.FXType.FXT_Fire01);
                break;
            case BeltItem.EffectTypeEnum.ETE_ICE:
                m_effect = EffectsManager.instance.getEffect(Vector2.zero, Quaternion.identity, EffectsManager.FXType.FXT_Ice01);
                break;
            case BeltItem.EffectTypeEnum.ETE_SHOCK:
                m_effect = EffectsManager.instance.getEffect(Vector2.zero, Quaternion.identity, EffectsManager.FXType.FXT_Shock01);
                break;

        }
        

        if (m_effect != null)
        {
            m_effect.GetComponent<SpriteRenderer>().sortingLayerID = m_enemyView.GetComponent<SpriteRenderer>().sortingLayerID;
            m_effect.GetComponent<SpriteRenderer>().sortingOrder = m_enemyView.GetComponent<SpriteRenderer>().sortingOrder + 1;

            m_effect.GetComponent<Transform>().SetParent(this.GetComponent<Transform>(), false);

            m_effect.triggerEffect();
        }
        if (propagate &&
            m_effectType != BeltItem.EffectTypeEnum.ETE_NORMAL &&
            m_effectType != BeltItem.EffectTypeEnum.ETE_HEAVY &&
            m_effectType == m_enemyView.m_type)
        {
            propagateDir(0, -1);
            propagateDir(0, 1);
            propagateDir(-1, 0);
            propagateDir(1, 0);
        }
        
        m_states[m_curState].resetState();
        m_curState = (int)StateEnum.SE_DEAD;
        
    }

    public bool isType(BeltItem.EffectTypeEnum effectType)
    {
        return effectType == m_enemyView.m_type;
    }

    public void propagateDir(int colValue, int rowValue)
    {
        int curCol = m_node.m_col + colValue;
        int curRow = m_node.m_row + rowValue;
        pathNode tempNode = SceneManager.instance.getEnemyManager().getNode(curCol, curRow);
        while (tempNode != null && tempNode.m_refEnemy != null && tempNode.m_refEnemy.isType(m_enemyView.m_type))
        {
            tempNode.m_refEnemy.kill(m_effectType);
            curCol += colValue;
            curRow += rowValue;
            tempNode = SceneManager.instance.getEnemyManager().getNode(curCol, curRow);
        }
    }
}
