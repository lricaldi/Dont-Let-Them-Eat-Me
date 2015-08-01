using UnityEngine;
using System.Collections;

public class EnemyDeadState : StateBaseWithActions<Enemy>
{
    private enum ActionEnum { AE_ANIMATE, AE_FALL, AE_Length }

    public EnemyDeadState(Enemy refEnemy) : base(refEnemy)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null, SceneManager.instance.hashIDs.dead, m_refObj.getViewAnimator()); // never ends.
        m_actions[(int)ActionEnum.AE_FALL]      = new movFall();
    }

    public override void initState()
    {
        Transform rotTransform = m_refObj.doesEffectRotate() ? m_refObj.GetComponent<Transform>() : m_refObj.getView().GetComponent<Transform>();
        
        Vector2 startSpeed = new Vector2(Random.Range(-0.25f, 0.25f), 1f).normalized * 8;
        
        ((movFall)m_actions[(int)ActionEnum.AE_FALL]).setup(m_refObj.GetComponent<Transform>(), startSpeed,
                                                                -20f, ViewManager.instance.getBottomScreenY() - 1,
                                                                Random.Range(100, 150), rotTransform);
        
        m_refObj.freeNode();
        SceneManager.instance.getEnemyManager().removeFromPath(m_refObj);

        EffectsManager.instance.getEffect(m_refObj.GetComponent<Transform>().position, Quaternion.identity, EffectsManager.FXType.FXT_Hit01).triggerEffect();
        

        m_curAction = (int)ActionEnum.AE_ANIMATE;
        curStep     = StateStep.SSRuning;
    }
 
    public override void endState()
    {
        m_refObj.cleanUp();
    }
}
