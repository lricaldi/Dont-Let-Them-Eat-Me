using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadePanel : MonoBehaviour 
{

    public enum FadeEnum { FE_IN, FE_OUT, FE_DONE}
    private FadeEnum m_fadeType;

    private BlackFade m_fadeAction;

    void Awake()
    {
        m_fadeType = FadeEnum.FE_DONE;
        m_fadeAction = new BlackFade();
    }

    public void fadeInOut(FadeEnum fadeType)
    {
        if (fadeType == FadeEnum.FE_IN)
        {
            this.gameObject.SetActive(true);
            m_fadeType = FadeEnum.FE_IN;
            m_fadeAction.setup(GetComponent<Image>(), 1, 1, 0);
        }
        else if (fadeType == FadeEnum.FE_OUT)
        {
            this.gameObject.SetActive(true);
            m_fadeType = FadeEnum.FE_OUT;
            m_fadeAction.setup(GetComponent<Image>(), 1, 0, 1);
        }
    }

	// Update is called once per frame
	void Update () 
    {
        if (m_fadeType != FadeEnum.FE_DONE)
        {
            m_fadeAction.update(Time.deltaTime);
            if (m_fadeAction.isDone())
            {
                if(m_fadeType == FadeEnum.FE_IN)
                {
                    this.gameObject.SetActive(false);
                }
                m_fadeType = FadeEnum.FE_DONE;
                
                
            }
        }
	}
}
