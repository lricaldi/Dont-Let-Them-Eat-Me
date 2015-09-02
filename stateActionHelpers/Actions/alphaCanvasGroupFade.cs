using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class alphaCanvasGroupFade : StateActionBase
{
    private CanvasGroup m_cg;
    private float       m_speed;
    private float       m_alphaFrom;
    private float       m_alphaTo;

    private float       m_time;

    public alphaCanvasGroupFade(){}

    public alphaCanvasGroupFade(CanvasGroup cg, float speed, float alphaFrom, float alphaTo)
    {
        setup(cg, speed, alphaFrom, alphaTo);

    }

    public void setup(CanvasGroup cg, float speed, float alphaFrom, float alphaTo)
    {
        m_cg        = cg;
        m_alphaFrom = alphaFrom;
        m_alphaTo   = alphaTo;
        m_speed     = speed;
        m_done      = false;
        m_time      = 0;

        m_cg.alpha = m_alphaFrom;
       
    }

    public override void update(float delta)
    {
        m_cg.alpha = Mathf.Lerp(m_alphaFrom, m_alphaTo, m_time);
        m_time += m_speed * delta;

        if (m_time > 1)
        {
            m_done = true;
        }
    }

}
