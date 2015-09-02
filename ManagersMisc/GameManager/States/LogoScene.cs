using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoScene : StateBaseWithActions<GameManager>
{
    public enum ActionEnum { AE_FADEIN, AE_WAIT, AE_FADEOUT, AE_WAITTWO, AE_LENGTH }
    
    
    private GameObject m_imageObj;
    private GameObject m_logoImg;
    public AudioSource m_audioSource;
    public AudioClip    m_logoSound;
    
    
    public LogoScene()
        : base(null)
    {

        m_actions = new StateActionBase[(int)ActionEnum.AE_LENGTH];
        
        m_actions[(int)ActionEnum.AE_FADEIN] = new BlackFade();
        m_actions[(int)ActionEnum.AE_WAIT] = new waitTime(3);
        m_actions[(int)ActionEnum.AE_FADEOUT] = new BlackFade();
        m_actions[(int)ActionEnum.AE_WAITTWO] = new waitTime(1.5f);
    }

    public override void initState()
    {
        m_audioSource = GameObject.Find("LogoSound").GetComponent<AudioSource>();
        m_logoSound = Resources.Load<AudioClip>("Audio/Effects/robotSound");

        m_imageObj = GameObject.Find("FadeScreen");
        m_logoImg = GameObject.Find("RobotOnLogo");

        Image fadeScreen = m_imageObj.GetComponent<Image>();

        m_logoImg.GetComponent<Transform>().localPosition = Vector3.zero;
        ((BlackFade)m_actions[(int)ActionEnum.AE_FADEOUT]).setup(fadeScreen, 2f, 0, 1);
        ((BlackFade)m_actions[(int)ActionEnum.AE_FADEIN]).setup(fadeScreen, 2f, 1, 0);
        
        

        m_audioSource.loop = false;
        m_audioSource.clip = m_logoSound;
        m_audioSource.Play();

        curStep = StateStep.SSRuning;
    }

    public override void endState()
    {
        Application.LoadLevel("Menu");
    }

    
}
