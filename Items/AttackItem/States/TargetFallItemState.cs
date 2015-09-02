using UnityEngine;
using System.Collections;

public class TargetFallItemState : StateBaseWithActions<AttackItem>
{
    private enum ActionEnum { AE_GODOWN, AE_HIT, AE_BOUNCEFALL,  AE_Length }

    public TargetFallItemState(AttackItem refItem):base(refItem)
    {
        m_actions                                   = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_GODOWN]        = new movAtoB();
        m_actions[(int)ActionEnum.AE_HIT]           = new waitTime(0.05f);
        m_actions[(int)ActionEnum.AE_BOUNCEFALL]    = new movFall();

       
    }

    public override void initState()
    {
       ((movAtoB)m_actions[(int)ActionEnum.AE_GODOWN]).setup(m_refObj.gameObject, m_refObj.GetComponent<Transform>().position, 
                                                            SceneManager.instance.getEnemyTarget().GetComponent<Transform>().position,
                                                            2.5f, 0);

      
        Vector2 startSpeed = new Vector2(Random.Range(-0.25f, 0.25f), 1f).normalized * 8;
        ((movFall)m_actions[(int)ActionEnum.AE_BOUNCEFALL]).setup(m_refObj.GetComponent<Transform>(), startSpeed,
                                                                -20f, ViewManager.instance.getBottomScreenY() - 1,
                                                                Random.Range(100, 150));
      
        m_curAction = (int)ActionEnum.AE_GODOWN;
        curStep     = StateStep.SSRuning;
    }

    protected override bool actionDone()
    {
        if (m_curAction == (int)ActionEnum.AE_HIT)
        {
            SceneManager.instance.getEnemyTarget().killAttached(m_refObj.getEffectType());
        }
        else if (m_curAction == (int)ActionEnum.AE_GODOWN)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.m_hitEnemy, false, 1);
        }
        return base.actionDone();
    }

    public override void endState()
    {
        m_refObj.doneAttacking();
        resetState();
    }
}
