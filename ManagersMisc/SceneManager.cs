using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour 
{
    public enum SceneEvent { SE_TARGETWON, SE_TARGETDIED, SE_TARGETHEALTHCHANGE, SE_ATTACKITEMLAUCHED, SE_ATTACKITEMDONE }

    public delegate void    sceneEventHandler(SceneEvent sceneEvent, int valueone);
    public event            sceneEventHandler sceneEvent;

    public static SceneManager instance = null;

    public  HashIDs         hashIDs { get; set; }
    private GameKeyboard    m_gameKB;
    private EnemyTarget     m_target;
    private EnemyManager    m_enemyMan;
    
    private ItemsBelt       m_itemsBelt;
    private UIScript        m_UIScript;

    private bool            m_gameFinished;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        hashIDs = GetComponent<HashIDs>();
        
        m_gameFinished = false;
    }


    

    public UIScript getUIScript()
    {
        if (m_UIScript == null)
        {
            m_UIScript = GameObject.Find("Canvas").GetComponent<UIScript>();
        }
        return m_UIScript;
    }
    public ItemsBelt getItemsBelt()
    {
        if (m_itemsBelt == null)
        {
            m_itemsBelt = GameObject.Find("ItemsBelt").GetComponent<ItemsBelt>();
        }
        return m_itemsBelt;
    }


    public void targetDied()
    {
        if (!m_gameFinished)
        {
            m_gameFinished = true;
            sceneEvent(SceneEvent.SE_TARGETDIED, 0);
        }
    }

    public void attackItemLaunched()
    {
        sceneEvent(SceneEvent.SE_ATTACKITEMLAUCHED, 0);
    }

    public void attackItemDone()
    {
        sceneEvent(SceneEvent.SE_ATTACKITEMDONE, 0);
    }

    public void targetWon()
    {
        if (!m_gameFinished)
        {
            m_gameFinished = true;
            sceneEvent(SceneEvent.SE_TARGETWON, 0);
        }
    }

    public void lockInput(bool doLock)
    {
        m_gameKB.lockKeyboard(doLock);
    }

    public void clearInputWord()
    {
        m_gameKB.clearInputWord();
    }

    public void targetHealthUpdate(int healthValue)
    {
        sceneEvent(SceneEvent.SE_TARGETHEALTHCHANGE, healthValue);
    }

    public void registerForSceneEvent(sceneEventHandler sceneEventDelegate)
    {
        sceneEvent += sceneEventDelegate;
    }

    public EnemyTarget getEnemyTarget()
    {
        return m_target;
    }
    
    public EnemyManager getEnemyManager()
    {
        return m_enemyMan;
    }

    void Start()
    {
        m_target        = GameObject.Find("EnemyTarget").GetComponent<EnemyTarget>();
        m_enemyMan      = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        m_gameKB        = GameObject.Find("GameKeyboard").GetComponent<GameKeyboard>();

        m_gameKB.lockKeyboard(false);
    }
   
    public bool isGameFinished()
    {
        return m_gameFinished;
    }

    public void showWordClue(int beltItemPos, bool doSHow)
    {
        getUIScript().showClue(getItemsBelt().getBeltItemName(beltItemPos), true);
    }

  



}
