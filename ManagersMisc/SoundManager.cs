using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


    public AudioSource fxSourceChOne;
    public AudioSource fxSourceChTwo;
    public AudioSource musicSource;

    [HideInInspector]
    public AudioClip    m_music;
    [HideInInspector]
    public AudioClip    m_music02;
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
        m_music         = Resources.Load<AudioClip>("Audio/Music/Music01");
        m_music02       = Resources.Load<AudioClip>("Audio/Music/Music02");
        m_musicTitle    = Resources.Load<AudioClip>("Audio/Music/TitleScreenMusicPlaceHolder");
        

        m_correctWord   = Resources.Load<AudioClip>("Audio/Effects/correctWord");
        m_enemyJump     = Resources.Load<AudioClip>("Audio/Effects/enemyJump");
        m_hitEnemy      = Resources.Load<AudioClip>("Audio/Effects/hitEnemy");
        m_shootUp       = Resources.Load<AudioClip>("Audio/Effects/shootUp");
        m_targetDead    = Resources.Load<AudioClip>("Audio/Effects/targetDead");

        m_wrongInput    = Resources.Load<AudioClip>("Audio/Effects/wrongInput");

        PlayMusic(m_music02, true);
    }

    public void PlaySound(AudioClip clip, bool loop, int channel)
    {
        if (channel == 1)
        {
            fxSourceChOne.clip = clip;
            fxSourceChOne.loop = loop;
            fxSourceChOne.Play();
        }
        else
        {
            fxSourceChTwo.clip = clip;
            fxSourceChTwo.loop = loop;
            fxSourceChTwo.Play();
        }
       
    
    }

    public void PlayMusic(AudioClip clip, bool loop)
    {
        musicSource.clip = clip;
        musicSource.loop = loop;
        musicSource.Play();
    }

}
