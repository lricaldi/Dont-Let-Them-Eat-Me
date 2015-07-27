using UnityEngine;
using System.Collections;

public class TargetHappy : StateBaseWithActions<EnemyTarget>
{
    private enum ActionEnum { AE_ANIMATE, AE_WOBBLE, AE_Length }

    public TargetHappy(EnemyTarget refTarget):base(refTarget)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null,  SceneManager.instance.hashIDs.happy, m_refObj.getViewAnimator()); // never ends.
        m_actions[(int)ActionEnum.AE_WOBBLE]    = new wobble(m_refObj.getViewTransform(), 5, 1.5f); // never ends.
    }

    public override void initState()
    {
        m_curAction = (int)ActionEnum.AE_ANIMATE;
        curStep     = StateStep.SSRuning;
    }
    
    public override void runState()
    {
        if (m_refObj.getNumAttachedEnemies() > 0)
        {
            m_actions[m_curAction].forceDone();
        }
        base.runState();
    }

    public override void endState()
    {
        m_refObj.setCurrentState(EnemyTarget.StateEnum.SE_ATTACKED);
        resetState();
    }
}
