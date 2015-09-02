using UnityEngine;
using System.Collections;

public class TargetSad : StateBaseWithActions<EnemyTarget>
{

	private enum ActionEnum { AE_ANIMATE, AE_WOBBLE, AE_Length }

    public TargetSad(EnemyTarget refTarget):base(refTarget)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null,  SceneManager.instance.hashIDs.sad, m_refObj.getViewAnimator()); // never ends.
        m_actions[(int)ActionEnum.AE_WOBBLE]    = new wobble(m_refObj.getViewTransform(), 5, 0.8f); // never ends.

    }

    public override void initState()
    {
        m_curAction = (int)ActionEnum.AE_ANIMATE;
        curStep     = StateStep.SSRuning;
    }
    
    public override void runState(float delta)
    {
        if (m_refObj.getNumAttachedEnemies() > 0)
        {
            m_actions[m_curAction].forceDone();
        }
        base.runState(delta);
    }

    public override void endState()
    {

        m_refObj.setCurrentState(EnemyTarget.StateEnum.SE_ATTACKED);
        resetState();
    }
}
