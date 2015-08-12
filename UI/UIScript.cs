using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour 
{

    private const int WAIT_TIME_KB_HINT = 10;

    private Text        m_wordText;
    private Text        m_clueText;
    private GameObject  m_shotTouch;
    private GameObject  m_toMenuButton;
    private GameObject  m_youWon;
    private GameObject  m_youLost;

    public KeyboardHintPanel    m_KBUseHint;
    private float               m_timerKBUseHint;
    private bool                m_kbHintShowed;
    
    private TargetHealth        m_targetHealth;
    private EnemiesCounter      m_enemyCounter;
    private FadePanel           m_fadePanel;




    void Start()
    {
        m_wordText          = GetComponent<Transform>().Find("ItemNameInput").GetComponent<Text>();
        m_clueText          = GetComponent<Transform>().Find("nameClue").GetComponent<Text>();
        m_toMenuButton      = GetComponent<Transform>().Find("ToMenu").gameObject;
        m_youWon            = GetComponent<Transform>().Find("YouWon").gameObject;
        m_youLost           = GetComponent<Transform>().Find("YouLost").gameObject;

        m_targetHealth      = GetComponent<Transform>().Find("TargetHealth").GetComponent<TargetHealth>();
       

        m_shotTouch         = GetComponent<Transform>().Find("shotTouch").gameObject;
        
        m_toMenuButton.SetActive(false);
        m_youWon.SetActive(false);
        m_youLost.SetActive(false);

        m_clueText.active   = false;
        m_timerKBUseHint    = 0;
        m_kbHintShowed      = false;

        registerToKeyboard();
        SceneManager.instance.registerForSceneEvent(new SceneManager.sceneEventHandler(sceneEvent));
        
    }

    private void registerToKeyboard()
    {
        GameKeyboard keyboard   = GameObject.Find("GameKeyboard").GetComponent<GameKeyboard>();
        keyboard.registerForKeyboardEvent(new GameKeyboard.keyboardEventHandler(keyboardEvent));
        
    }

    void Update()
    {
        if ((Input.touchCount > 0 || Input.GetKeyDown("mouse 0")))
        {
            m_timerKBUseHint = 0;
            if(m_KBUseHint.Showing)
            {
                m_KBUseHint.ForceEndHint();
               
            }
            
        }
        if (!m_kbHintShowed && !SceneManager.instance.isGameFinished())
        {
            m_timerKBUseHint += Time.deltaTime;
            if (m_timerKBUseHint > WAIT_TIME_KB_HINT)
            {
                m_KBUseHint.StartHint();
                m_kbHintShowed = true;
            }
        }
    }

    private void sceneEvent(SceneManager.SceneEvent sceneEvent, int valueOne)
    {
        switch (sceneEvent)
        {
            case SceneManager.SceneEvent.SE_TARGETHEALTHCHANGE:
                m_targetHealth.setHealth(valueOne);
                break;
            case SceneManager.SceneEvent.SE_TARGETWON:
                m_youWon.SetActive(true);
                m_toMenuButton.SetActive(true);
                
                break;
            case SceneManager.SceneEvent.SE_TARGETDIED:
                m_youLost.SetActive(true);
                m_toMenuButton.SetActive(true);
               
                break;
        }
       
    }

    public void fadeIn()
    {
        if (m_fadePanel == null)
        {
            m_fadePanel = GetComponent<Transform>().Find("FadePanel").GetComponent<FadePanel>();
        }

        m_fadePanel.fadeInOut(FadePanel.FadeEnum.FE_IN);
       
    }

    private void keyboardEvent(GameKeyboard.KeyboardEvent kbEvent, string valueOne)
    {
       
        switch (kbEvent)
        {
            case GameKeyboard.KeyboardEvent.KE_WORDUPDATE:
                m_wordText.text = valueOne;
                break;
        }
        
    }

    public void updateInputWordColor(Color inputWordColor)
    {
        m_wordText.color = inputWordColor;
    }

    public void goToMenu()
    {
        if (m_fadePanel == null)
        {
            m_fadePanel = GetComponent<Transform>().Find("FadePanel").GetComponent<FadePanel>();
        }

        m_fadePanel.fadeInOut(FadePanel.FadeEnum.FE_OUT);

        Invoke("loadMenu", 1);
    }

    private void loadMenu()
    {
        Application.LoadLevel("Menu");
    }

    public void beltItemTouch(int beltIndex)
    {
        if (beltIndex == ItemsBelt.FOCUS_POS_INDEX && SceneManager.instance.getItemsBelt().hasWordMatch())
        {
            SceneManager.instance.getItemsBelt().trySubmitWord();
        }
        else
        {
            showClue(SceneManager.instance.getItemsBelt().getBeltItemName(beltIndex), true);
        }
    }

    public EnemiesCounter getEnemyCounter()
    {
        if (m_enemyCounter == null)
        {
             m_enemyCounter = GetComponent<Transform>().Find("EnemiesCounter").GetComponent<EnemiesCounter>();
        }
        return m_enemyCounter;
    }

    public TargetHealth getTargetHealth()
    {
        return m_targetHealth;
    }

    public void beltItemRelease(int beltIndex)
    {
        showClue("", false);
    }

    public void showClue(string clue, bool doShow)
    {
        if (m_clueText.active == doShow) return;
        m_clueText.text = clue;
        m_clueText.active = doShow;
        m_wordText.active = !doShow;
    }
    

    public void showShotTouch()
    {
        m_shotTouch.GetComponent<Animator>().SetTrigger("animate");
    }
    
}
