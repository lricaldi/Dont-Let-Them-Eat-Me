using UnityEngine;
using System.Collections;



public class GameManager : MonoBehaviour 
{

    public enum GameManagerState { GMS_Logo, GMS_IntroMenu, GMS_InGame, GMS_AdShow, GMS_Length }

    public static GameManager   instance = null;
    StateBase<GameManager>[]    m_gameStates;
	private int                 m_curState;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}


	void Start()
	{
		m_gameStates                                        = new StateBase<GameManager>[(int)GameManagerState.GMS_Length];

        m_gameStates[(int)GameManagerState.GMS_Logo]        = new LogoScene();
		m_gameStates[(int)GameManagerState.GMS_IntroMenu]	= new IntroMenuScene();
		m_gameStates[(int)GameManagerState.GMS_InGame]	    = new InGameScene();
		m_gameStates[(int)GameManagerState.GMS_AdShow]	    = new LevelOutroScene();

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 30;
        setNextState();
	}


	void Update()
	{
		m_gameStates[m_curState].updateState(Time.deltaTime);
	}

    private void setNextState()
    {
        if (Application.loadedLevelName.Contains("Logo"))
        {
            QualitySettings.vSyncCount = 1;
            m_curState = (int)GameManagerState.GMS_Logo;
        }
        else if (Application.loadedLevelName.Contains("Game"))
        {
            QualitySettings.vSyncCount = 0;
            m_curState = (int)GameManagerState.GMS_InGame;
        }
        else if (Application.loadedLevelName.Equals("Menu"))
        {
            QualitySettings.vSyncCount = 1;
            m_curState = (int)GameManagerState.GMS_IntroMenu;
        }
       
        else if (Application.loadedLevelName.Equals("AdScreen"))
        {
            m_curState = (int)GameManagerState.GMS_AdShow;
        }
        else
        {
            // Invalid level.
        }
    }
	void OnLevelWasLoaded(int level)
	{
		// for some reason this function gets called twice, first time is an non initialized object, the second one the objects are fine 
		// so I will skip any call where the states are null, other people have this issue with Unity.
		if (m_gameStates == null)
		{
			return;
		}

		m_gameStates[m_curState].resetState();

        setNextState();

		
	}

    
}
