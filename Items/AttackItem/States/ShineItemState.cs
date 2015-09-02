using UnityEngine;
using System.Collections;

public class ShineItemState : StateBaseWithActions<AttackItem>
{
    private enum ActionEnum { AE_WAIT, AE_Length }

    public ShineItemState(AttackItem refItem):base(refItem)
    {
        m_actions                           = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_WAIT]  = new waitTime(0.5f); 
    }

    public override void initState()
    {
        m_refObj.setLayer("foreground", 10);
        
      
        m_refObj.getLaunchEffect().GetComponent<Animator>().SetTrigger("triggerEffect");
        m_curAction = (int)ActionEnum.AE_WAIT;
        curStep     = StateStep.SSRuning;
    }
 
    public override void endState()
    {
        m_refObj.getLaunchEffect().GetComponent<Animator>().ResetTrigger("triggerEffect");
        m_refObj.setCurState((int)AttackItem.StateEnum.SE_LAUNCH);
        resetState();
    }
}
