using UnityEngine;
using System.Collections;

public class EnemyIdleState : StateBaseWithActions<Enemy>
{
    private const float NORMAL_MOVE_WAIT = 20;
    private const float RANDOM_MOVE_RANGE = 5;
    private const float FAST_MOVE_WAIT = 0.05f;

    private enum ActionEnum { AE_ANIMATE, AE_WAIT, AE_Length }

    private bool             m_targetWaiting;
    private Enemy.StateEnum  m_nextState;

    public EnemyIdleState(Enemy refEnemy):base(refEnemy)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null, SceneManager.instance.hashIDs.idle, m_refObj.getViewAnimator()); // never ends.
        m_actions[(int)ActionEnum.AE_WAIT]      = new waitTime(NORMAL_MOVE_WAIT);
    }

    public override void initState()
    {
        m_refObj.setUpdateSpeed(0.03f);
        ((waitTime)m_actions[(int)ActionEnum.AE_WAIT]).setup(m_refObj.isFastMoveNode() ? FAST_MOVE_WAIT : NORMAL_MOVE_WAIT);
        
        m_targetWaiting = false;
        m_curAction     = (int)ActionEnum.AE_ANIMATE;
        
        curStep         = StateStep.SSRuning;
    }

    
    protected override bool actionDone()
    {
        if (m_curAction == (int)ActionEnum.AE_ANIMATE)
        {
            m_refObj.setUpdateSpeed(m_refObj.isFastMoveNode() ? FAST_MOVE_WAIT : NORMAL_MOVE_WAIT);
        }
        else if (m_curAction == (int)ActionEnum.AE_WAIT)
        {
            m_refObj.setUpdateSpeed(0.03f);
            m_curAction--;
            ((waitTime)m_actions[(int)ActionEnum.AE_WAIT]).setup(FAST_MOVE_WAIT);
            
            if (m_refObj.isCurNodeEndNode())
            {
                if (!m_targetWaiting)
                {
                    m_targetWaiting = true;
                    ((waitTime)m_actions[(int)ActionEnum.AE_WAIT]).setup(Random.Range(0, RANDOM_MOVE_RANGE));
                }
                else 
                {
                    if (SceneManager.instance.getEnemyTarget().canAttachEnemy())
                    {
                        m_nextState = Enemy.StateEnum.SE_EAT;
                        curStep = StateStep.SSEnd;
                    }
                    else
                    {
                        ((waitTime)m_actions[(int)ActionEnum.AE_WAIT]).setup(Random.Range(0, RANDOM_MOVE_RANGE));
                    }
                }
               
            }
            else if (m_refObj.nextNodeAvailable())
            {
                m_nextState = Enemy.StateEnum.SE_MOVEUP;
                curStep = StateStep.SSEnd;
            }
        }
        return true;
    }

    public override void endState()
    {
        m_refObj.setCurrentState(m_nextState);
        resetState();
    }
}
