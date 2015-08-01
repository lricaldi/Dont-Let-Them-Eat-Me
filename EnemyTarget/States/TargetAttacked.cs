using UnityEngine;
using System.Collections;

public class TargetAttacked : StateBaseWithActions<EnemyTarget>
{
    private const float ATTACK_INTERVAL         = 3;
    private const float CRUMB_INTERVAL          = 0.5f;
    private const int ATTACK_VALUE_PER_ENEMY    = -1;

    private enum ActionEnum { AE_ANIMATE, AE_WOBBLE, AE_Length }
    
    private EnemyTarget.StateEnum   m_nextState;
    private float                   m_timer;
    private float                   m_timerCrumbs;

    
   
    public TargetAttacked(EnemyTarget refTarget):base(refTarget)
    {

        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null,  SceneManager.instance.hashIDs.attacked, m_refObj.getViewAnimator());
        m_actions[(int)ActionEnum.AE_WOBBLE] = new movOscilate(); 
    }

    public override void initState()
    {

        ((movOscilate)m_actions[(int)ActionEnum.AE_WOBBLE]).setup(m_refObj.getViewTransform().gameObject, 0, 0.1f, 0, 5f);
        
        m_timer     = 0;
        m_timerCrumbs = 0;
        m_curAction = (int)ActionEnum.AE_ANIMATE;
        curStep     = StateStep.SSRuning;
   
    }
    
    public override void runState()
    {
        if (m_refObj.getNumAttachedEnemies() <= 0)
        {
            m_actions[m_curAction].forceDone();

            if (m_refObj.getHealth() < EnemyTarget.SAD_HEALTH)
            {
                m_nextState = EnemyTarget.StateEnum.SE_SAD;
            }
            else if (m_refObj.getHealth() < EnemyTarget.OK_HEALTH)
            {
                m_nextState = EnemyTarget.StateEnum.SE_OK;
            }
            else
            {
                 m_nextState = EnemyTarget.StateEnum.SE_HAPPY;
            }
        }
        else
        {
            m_timer += Time.deltaTime;
            m_timerCrumbs += Time.deltaTime;

            if (m_timerCrumbs > CRUMB_INTERVAL)
            {
                Vector2 newPos = Random.insideUnitCircle;
                TargetCrumb crumb = InstanceFactory.instance.getTargetCrumb(newPos + (Vector2)m_refObj.GetComponent<Transform>().position, Quaternion.identity);
                crumb.setup();
                m_timerCrumbs = 0;
            }
            if (m_timer > ATTACK_INTERVAL)
            {
                

                int totalDamage = m_refObj.getNumAttachedEnemies() * ATTACK_VALUE_PER_ENEMY;
                m_refObj.addToHealth(totalDamage);
                m_timer         = 0;
            }
        }
        base.runState();
    }

    public override void endState()
    {
        m_refObj.setCurrentState(m_nextState);
        resetState();
    }
}
