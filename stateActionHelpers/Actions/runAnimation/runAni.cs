using UnityEngine;
using System.Collections;

public class runAni : StateActionBase 
{

    private IAniEvent m_aniEventObj;
    private int         m_aniHashId;
    private Animator    m_anim;

    private bool m_started;

	public override void reset()
	{
        m_started = false;
		base.reset();
	}

    // If null is passed for aniEventObj, the action is done when the animation is triggered

    public runAni(IAniEvent aniEventObj, int aniHashId, Animator anim)
	{
        setup(aniEventObj, aniHashId, anim);
	}

    public void setup(IAniEvent aniEventObj, int aniHashId, Animator anim)
	{
        m_aniEventObj   = aniEventObj;
        m_aniHashId     = aniHashId;
        m_anim          = anim;
       
		reset();
	}
	
    public override void forceEndAction()
    {
        m_anim.ResetTrigger(m_aniHashId);
        base.forceEndAction();
     
    }

	public override void update()
	{
        if (!m_started)
        {
            if (m_aniEventObj != null) { m_aniEventObj.getAniDone(); }
            m_anim.SetTrigger(m_aniHashId);
            m_started = true;
        }
        else if (m_aniEventObj == null ||  m_aniEventObj.getAniDone() > 0)
        {
            m_anim.ResetTrigger(m_aniHashId);
            m_done = true;
        }

        
	}
}
