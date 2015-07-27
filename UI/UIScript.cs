using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    private Text        m_wordText;
    private Text        m_debugHealthText;
    private GameObject  m_gameOver;


    void Start()
    {
        m_debugHealthText   = GetComponent<Transform>().Find("DebugHealth").GetComponent<Text>();
        m_wordText          = GetComponent<Transform>().Find("ItemNameInput").GetComponent<Text>();
        m_gameOver          = GetComponent<Transform>().Find("ToMenu").gameObject;
        m_gameOver.SetActive(false);

        registerToKeyboard();
        SceneManager.instance.registerForSceneEvent(new SceneManager.sceneEventHandler(sceneEvent));
    }

    private void registerToKeyboard()
    {
        GameKeyboard keyboard   = GameObject.Find("GameKeyboard").GetComponent<GameKeyboard>();
        keyboard.wordUpdate     += new GameKeyboard.wordUpdatedEventHandler(wordUpdated);
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

    private void wordUpdated(string newWord)
    {
        m_wordText.text = newWord;
    }

    public void pressStartButtonTwo()
    {
        Application.LoadLevel("Menu");
    }
}
