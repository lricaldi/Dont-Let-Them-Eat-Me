using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlackFade : StateActionBase{

	private Image       m_img;
    private float       m_speed;
    private float       m_alphaFrom;
    private float       m_alphaTo;

    private float       m_time;

    public BlackFade(){}

    public BlackFade(Image img, float speed, float alphaFrom, float alphaTo)
    {
        setup(img, speed, alphaFrom, alphaTo);

    }

    public void setup(Image img, float speed, float alphaFrom, float alphaTo)
    {
        m_img       = img;
        m_alphaFrom = alphaFrom;
        m_alphaTo   = alphaTo;
        m_speed     = speed;
        m_done      = false;
        m_time      = 0;

        Color imgColor = img.color;
        imgColor.a = m_alphaFrom;
        img.color = imgColor;
       
    }

    public override void update(float delta)
    {
        Color imgColor = m_img.color;

        imgColor.a = Mathf.Lerp(m_alphaFrom, m_alphaTo, m_time);
        m_img.color = imgColor;

        m_time += m_speed * delta;

        if (m_time > 1)
        {
            m_done = true;
        }
    }
}
