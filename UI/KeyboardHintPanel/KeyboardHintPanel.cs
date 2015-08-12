using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class KeyboardHintPanel : ObjectWithStates<KeyboardHintPanel> 
{

    public enum StateEnum { SE_RUNHINT, SE_Length }

    public Transform m_finger;
    public Transform m_CKey;
    public Transform m_AKey;
    public Transform m_TKey;
    public Transform m_startPos;
    public Text m_text;

    private bool m_showing;

    public bool Showing
    {
        set
        {
            this.m_showing = value;
        }
        get
        {
            return this.m_showing;
        }
    }

    protected override void initStates()
    {
        m_states = new StateBase<KeyboardHintPanel>[(int)StateEnum.SE_Length];
        m_states[(int)StateEnum.SE_RUNHINT] = new ShowHintPanelState(this);
        m_curState = (int)StateEnum.SE_RUNHINT;
    }

    public void StartHint()
    {
        if (!m_showing)
        {
            m_states[(int)StateEnum.SE_RUNHINT].resetState();
            m_showing = true;
        }
    }

    public void ForceEndHint()
    {
        
        if (m_showing)
        {
            ((ShowHintPanelState)m_states[(int)StateEnum.SE_RUNHINT]).forceEnd();
        }
    }


    protected override void update()
    {
        if (m_showing && m_states != null)
        {
            base.update();
        }
    }

}
