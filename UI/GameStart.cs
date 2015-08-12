using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStart : MonoBehaviour 
{
    private const float TIME_SHOW_SAVEME = 2f;
    private const float TIME_SHOW_START = 2;
    private const float TIME_START_GAME = 2;
    
    private Image m_saveMe;
    private Image m_start;
    private Image m_target;
    


	// Use this for initialization
	void Start () 
    {
        SceneManager.instance.lockInput(true);
        m_saveMe    = GetComponent<Transform>().Find("SaveMe").GetComponent<Image>();
        m_start     = GetComponent<Transform>().Find("Start").GetComponent<Image>();
        m_target    = GetComponent<Transform>().Find("targetToSave").GetComponent<Image>();

        hideAll();

        Invoke("showSaveMe", TIME_SHOW_SAVEME);
        SceneManager.instance.getUIScript().fadeIn();
	}

    private void showSaveMe()
    {
        
        m_saveMe.enabled = true;
        m_target.enabled = true;
        Invoke("showStart", TIME_SHOW_START);
    }

    private void showStart()
    {
        m_start.enabled = true;
        Invoke("startGame", TIME_START_GAME);
    }


    private void startGame()
    {
        SceneManager.instance.getEnemyManager().startMakingEnemies(true);
        SceneManager.instance.lockInput(false);
        hideAll();
        gameObject.SetActive(false);
    }
    private void hideAll()
    {
        m_saveMe.enabled = false;
        m_start.enabled = false;
        m_target.enabled = false;

    }

	// Update is called once per frame
	void Update () {
	
	}

}
