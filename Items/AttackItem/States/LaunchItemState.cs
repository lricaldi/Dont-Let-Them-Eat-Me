using UnityEngine;
using System.Collections;

public class LaunchItemState : StateBaseWithActions<AttackItem>
{
    private enum ActionEnum { AE_SHOT_UP, AE_Length }

    public LaunchItemState(AttackItem refItem):base(refItem)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_SHOT_UP]   = new movAtoB();        
    }

    public override void initState()
    {
        //Debug.Log("LaunchItemState INIT");

        m_curAction = (int)ActionEnum.AE_SHOT_UP;
        ((movAtoB)m_actions[(int)ActionEnum.AE_SHOT_UP]).setup(m_refObj.gameObject, 
                                                                m_refObj.GetComponent<Transform>().position, 
                                                                new Vector2(0, 9), 2, 0);
       
        SoundManager.instance.PlaySound(SoundManager.instance.m_shootUp, false, 1);
       
        curStep     = StateStep.SSRuning;
    }
 
    public override void endState()
    {
        if(SceneManager.instance.getEnemyTarget().getNumAttachedEnemies() > 0)
        {
            m_refObj.setCurState((int)AttackItem.StateEnum.SE_FALL_ON_TARGET);
        }
        else
        {
            m_refObj.setCurState((int)AttackItem.StateEnum.SE_FALL_NORMAL);
        }
        
        resetState();
    }
}
