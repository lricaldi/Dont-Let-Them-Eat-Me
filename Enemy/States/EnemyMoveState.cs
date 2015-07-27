using UnityEngine;
using System.Collections;

public class EnemyMoveState : StateBaseWithActions<Enemy>
{
    private enum ActionEnum { AE_MOVE, AE_Length }

    public EnemyMoveState(Enemy refEnemy):base(refEnemy)
    {
        m_actions                           = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_MOVE]  = new movAtoB(); // never ends.               
    }

    public override void initState()
    {
        ((movAtoB)m_actions[(int)ActionEnum.AE_MOVE]).setup(m_refObj.gameObject,
                                                                m_refObj.getCurNodePos(),
                                                                m_refObj.getNextNodePos(),
                                                                2, 0);
        m_curAction = (int)ActionEnum.AE_MOVE;
        curStep     = StateStep.SSRuning;
    }
 
    public override void endState()
    {
        m_refObj.moveToNextNode();
        m_refObj.setCurrentState(Enemy.StateEnum.SE_IDLE);
        resetState();
    }
}
