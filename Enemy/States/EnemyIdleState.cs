using UnityEngine;
using System.Collections;

public class EnemyIdleState : StateBaseWithActions<Enemy>
{
    private const float NORMAL_MOVE_WAIT = 6;
    private const float RANDOM_MOVE_RANGE = 3;
    private const float FAST_MOVE_WAIT = 0.05f;

    private enum ActionEnum { AE_ANIMATE, AE_WAIT, AE_Length }

    private bool waitingToMove;
    private float m_waitTime;

    public EnemyIdleState(Enemy refEnemy):base(refEnemy)
    {

        m_waitTime  = FAST_MOVE_WAIT;
        
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null, SceneManager.instance.hashIDs.idle, m_refObj.getViewAnimator()); // never ends.
        m_actions[(int)ActionEnum.AE_WAIT]      = new waitTime(m_waitTime);

        
    }

    public override void initState()
    {
        m_waitTime = (m_refObj.isFastMoveNode() || waitingToMove)? FAST_MOVE_WAIT : NORMAL_MOVE_WAIT;
        
        ((waitTime)m_actions[(int)ActionEnum.AE_WAIT]).setup(m_waitTime);

        waitingToMove   = false;
        m_curAction     = (int)ActionEnum.AE_ANIMATE;
        
        curStep         = StateStep.SSRuning;
    }
 
    public override void endState()
    {
        if (m_refObj.isCurNodeEndNode())
        {
            
            if (SceneManager.instance.getEnemyTarget().canAttachEnemy())
            {
                m_refObj.setCurrentState(Enemy.StateEnum.SE_EAT);
            }
        }
        else if (m_refObj.nextNodeAvailable())
        {
            m_refObj.setCurrentState(Enemy.StateEnum.SE_MOVEUP);
        }
        else
        {
            waitingToMove = true;
        }
        resetState();
    }
}
