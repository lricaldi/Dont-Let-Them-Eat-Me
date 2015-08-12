using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowHintPanelState : StateBaseWithActions<KeyboardHintPanel>
{

    private enum ActionEnum { AE_FADEIN, AE_WAITONE, AE_MOVETOC, AE_TYPEC, AE_WAITTWO, AE_MOVETOA, AE_TYPEA, AE_WAITTHREE, AE_MOVETOT, AE_TYPET, AE_WAITFOUR, AE_FADEOUT, AE_Length }

    public ShowHintPanelState(KeyboardHintPanel refPanel)
        : base(refPanel)
    {
        m_refObj.m_text.text = "";

        m_actions = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_FADEIN] = new alphaCanvasGroupFade();
        m_actions[(int)ActionEnum.AE_WAITONE] = new waitTime(1);
        m_actions[(int)ActionEnum.AE_MOVETOC] = new movAtoB(m_refObj.m_finger.gameObject, m_refObj.m_startPos.position, m_refObj.m_CKey.position, 2, 0);
        m_actions[(int)ActionEnum.AE_TYPEC] = new TypeText(m_refObj.m_text, "C", 0.01f);
        m_actions[(int)ActionEnum.AE_WAITTWO] = new waitTime(0.5f);
        m_actions[(int)ActionEnum.AE_MOVETOA] = new movAtoB(m_refObj.m_finger.gameObject, m_refObj.m_CKey.position, m_refObj.m_AKey.position, 2, 0);
        m_actions[(int)ActionEnum.AE_TYPEA] = new TypeText(m_refObj.m_text, "A", 0.01f);
        m_actions[(int)ActionEnum.AE_WAITTHREE] = new waitTime(0.5f);
        m_actions[(int)ActionEnum.AE_MOVETOT] = new movAtoB(m_refObj.m_finger.gameObject, m_refObj.m_AKey.position, m_refObj.m_TKey.position, 2, 0);
        m_actions[(int)ActionEnum.AE_TYPET] = new TypeText(m_refObj.m_text, "T", 0.01f);
        m_actions[(int)ActionEnum.AE_WAITFOUR] = new waitTime(1);
        m_actions[(int)ActionEnum.AE_FADEOUT] = new alphaCanvasGroupFade();
    }

    public override void initState()
    {
        m_refObj.m_text.text = "";
        m_refObj.m_finger.GetComponent<Transform>().position = m_refObj.m_startPos.position;
        m_curAction = (int)ActionEnum.AE_FADEIN;

        ((alphaCanvasGroupFade)m_actions[(int)ActionEnum.AE_FADEIN]).setup(m_refObj.GetComponent<CanvasGroup>(), 3, 0, 1);
        ((movAtoB)m_actions[(int)ActionEnum.AE_MOVETOC]).setup(m_refObj.m_finger.gameObject, m_refObj.m_startPos.position, m_refObj.m_CKey.position, 2, 0);
        ((TypeText)m_actions[(int)ActionEnum.AE_TYPEC]).setup(m_refObj.m_text, "C", 0.01f);
        ((movAtoB)m_actions[(int)ActionEnum.AE_MOVETOA]).setup(m_refObj.m_finger.gameObject, m_refObj.m_CKey.position, m_refObj.m_AKey.position, 2, 0);
        ((movAtoB)m_actions[(int)ActionEnum.AE_MOVETOT]).setup(m_refObj.m_finger.gameObject, m_refObj.m_AKey.position, m_refObj.m_TKey.position, 2, 0);

        curStep = StateStep.SSRuning;
    }

    public void forceEnd()
    {
        m_actions[m_curAction].reset();
        m_curAction = (int)ActionEnum.AE_FADEOUT;
        ((alphaCanvasGroupFade)m_actions[(int)ActionEnum.AE_FADEOUT]).setup(m_refObj.GetComponent<CanvasGroup>(), 3, 1, 0);

    }
    protected override bool actionDone()
    {
        if (m_curAction == (int)ActionEnum.AE_TYPEC)
        {
            ((TypeText)m_actions[(int)ActionEnum.AE_TYPEA]).setup(m_refObj.m_text, "A", 0.01f);
        }
        else if (m_curAction == (int)ActionEnum.AE_TYPEA)
        {
            ((TypeText)m_actions[(int)ActionEnum.AE_TYPET]).setup(m_refObj.m_text, "T", 0.01f);
        }
        else if (m_curAction == (int)ActionEnum.AE_WAITFOUR)
        {
            ((alphaCanvasGroupFade)m_actions[(int)ActionEnum.AE_FADEOUT]).setup(m_refObj.GetComponent<CanvasGroup>(), 3, 1, 0);
        }
        return base.actionDone();
    }

    public override void endState()
    {
        resetState();
        m_refObj.Showing = false;
    }

}
