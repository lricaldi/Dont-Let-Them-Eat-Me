using UnityEngine;
using System.Collections;

public class BlackFade : StateActionBase{

	private float m_alphaTarget;
	private bool m_fadeIn;
	private float m_fadeSpeed;
	private bool m_started;

	public BlackFade(bool fadeIn, float fadeSpeed)
	{
		setup(fadeIn, fadeSpeed);
	}

	public void setup(bool fadeIn, float fadeSpeed)
	{
        m_alphaTarget = (fadeIn) ? 0 : 1f;
		m_fadeIn = fadeIn;
		m_fadeSpeed = fadeSpeed;
		reset();
	}
	public override void reset()
	{
		base.reset();
		m_started = false;
	}

	public override void update() 
	{

       
		if (!m_started)
		{
            if (m_fadeIn)
			{
				ScreenFade.instance.hide(m_fadeSpeed, true);
               
			}
			else
			{
				ScreenFade.instance.show(m_fadeSpeed, true);
			}
			m_started = true;
		}
		else
		{
            m_done = ScreenFade.instance.getAlpha() == m_alphaTarget;
		}
	}
}
