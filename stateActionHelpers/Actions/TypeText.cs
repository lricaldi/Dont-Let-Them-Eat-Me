using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class TypeText : StateActionBase
{
    private string  m_textToType;
    private float   m_typeSpeed;
    private Text    m_textObj;
    

    private StringBuilder m_stringBuilder;

    private float   m_timer;
    private int     m_curChar;

	public TypeText(Text textObj, string textToType, float typeSpeed)
    {
        setup (textObj, textToType, typeSpeed);
    }

    public void setup(Text textObj, string textToType, float typeSpeed)
	{
	    m_textObj       = textObj;
		m_textToType    = textToType;
		m_typeSpeed     = typeSpeed;
        m_timer         = 0;
        m_curChar       = 0;
        m_stringBuilder = new StringBuilder(textObj.text);
        
        base.reset();
	}

    public override void update()
    {
        if (!m_done)
        {
            m_timer += Time.deltaTime;
            if (m_timer > m_typeSpeed)
            {
                if (m_curChar < m_textToType.Length)
                {
                    m_stringBuilder.Append(m_textToType[m_curChar++]);
                    m_textObj.text = m_stringBuilder.ToString();
                }
                else
                {
                    m_done = true;
                }
                m_timer = 0;
            }
        }
    }
}
