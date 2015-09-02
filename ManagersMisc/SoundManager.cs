using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


    public AudioSource[] fxSourceCh;
    /*public AudioSource fxSourceChTwo;
    public AudioSource fxSourceChThree;*/
    public AudioSource musicSource;

    [HideInInspector]
    public AudioClip    m_inGameMusic;
    [HideInInspector]
    public AudioClip    m_musicTitle;
    [HideInInspector]
    public  AudioClip   m_typeSound;
    [HideInInspector]
    public AudioClip    m_correctWord;
    [HideInInspector]
    public AudioClip    m_enemyJump;
    [HideInInspector]
    public AudioClip    m_hitEnemy;
    [HideInInspector]
    public AudioClip    m_shootUp;
    [HideInInspector]
    public AudioClip    m_targetDead;
    [HideInInspector]
    public AudioClip    m_wrongInput;
    
    


    public static SoundManager instance = null;


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
       

    }

    void Start()
    {


        m_typeSound     = Resources.Load<AudioClip>("Audio/Effects/TypeSound");
        m_inGameMusic   = Resources.Load<AudioClip>("Audio/Music/inGameMusic");
        m_musicTitle    = Resources.Load<AudioClip>("Audio/Music/TitleScreenMusicPlaceHolder");
        

        m_correctWord   = Resources.Load<AudioClip>("Audio/Effects/correctWord");
        m_enemyJump     = Resources.Load<AudioClip>("Audio/Effects/enemyJump");
        m_hitEnemy      = Resources.Load<AudioClip>("Audio/Effects/hitEnemy");
        m_shootUp       = Resources.Load<AudioClip>("Audio/Effects/shootUp");
        m_targetDead    = Resources.Load<AudioClip>("Audio/Effects/targetDead");

        m_wrongInput    = Resources.Load<AudioClip>("Audio/Effects/wrongInput");

        PlayMusic(m_inGameMusic, true);
    }

    public void PlaySound(AudioClip clip, bool loop, int channel)
    {
        if (channel < 0 || channel  >= fxSourceCh.Length)
        {
            channel = 0;
        }
       
            fxSourceCh[channel].clip = clip;
            fxSourceCh[channel].loop = loop;
            fxSourceCh[channel].Play();
        
       
    
    }

    public void PlayMusic(AudioClip clip, bool loop)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

}
