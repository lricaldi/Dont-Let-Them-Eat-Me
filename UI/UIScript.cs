using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    private Text        m_wordText;
    private Text        m_clueText;
    private Text        m_debugHealthText;
    private GameObject  m_shotTouch;
    private GameObject  m_gameOver;


    void Start()
    {
        m_debugHealthText   = GetComponent<Transform>().Find("DebugHealth").GetComponent<Text>();
        m_wordText          = GetComponent<Transform>().Find("ItemNameInput").GetComponent<Text>();
        m_clueText          = GetComponent<Transform>().Find("nameClue").GetComponent<Text>();
        m_gameOver          = GetComponent<Transform>().Find("ToMenu").gameObject;
        m_shotTouch         = GetComponent<Transform>().Find("shotTouch").gameObject;
        m_gameOver.SetActive(false);

        m_clueText.active = false;
        registerToKeyboard();
        SceneManager.instance.registerForSceneEvent(new SceneManager.sceneEventHandler(sceneEvent));
    }

    private void registerToKeyboard()
    {
        GameKeyboard keyboard   = GameObject.Find("GameKeyboard").GetComponent<GameKeyboard>();
        keyboard.registerForKeyboardEvent(new GameKeyboard.keyboardEventHandler(keyboardEvent));
        
    }
    
    private void sceneEvent(SceneManager.SceneEvent sceneEvent, int valueOne)
    {
        switch (sceneEvent)
        {
            case SceneManager.SceneEvent.SE_TARGETHEALTHCHANGE:
                m_debugHealthText.text = "" + valueOne;
                break;
            case SceneManager.SceneEvent.SE_TARGETWON:
            case SceneManager.SceneEvent.SE_TARGETDIED:
                m_gameOver.SetActive(true);
                break;
        }
       
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
        Application.LoadLevel("Menu");
    }

    public void beltTouch(int beltIndex)
    {
        SceneManager.instance.showWordClue(beltIndex);
       
    }

    public void showClue(string clue, bool doShow)
    {
        if (m_clueText.active == doShow) return;
        Debug.Log("ShowClue");
        m_clueText.text = clue;
        m_clueText.active = doShow;
        m_wordText.active = !doShow;
        if (doShow)
        {
            Invoke("hideClue", 0.8f);
        }
    }
    public void hideClue()
    {
        showClue("", false);
    }

    public void showShotTouch()
    {
        m_shotTouch.GetComponent<Animator>().SetTrigger("animate");
    
    }
    
}
