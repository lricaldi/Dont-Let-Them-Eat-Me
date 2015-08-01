using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class GameKeyboard : MonoBehaviour 
{
    private int         NUMB_INPUTKEYS          = 26;
    private int         LETTER_TO_ARRAY_OFFSET  = (int)'A';
    private const int   MAX_WORD_SIZE           = 32;


    public enum KeyboardEvent { KE_WORDUPDATE, KE_SUBMITWORD, KE_Length}

    public delegate void    keyboardEventHandler(KeyboardEvent KBEvent, string valueOne);
    public event            keyboardEventHandler keyboardEvent;


    private bool            m_keysLocked;
    private StringBuilder   m_word;
    private Vector2[]       m_keyPos;
    private Vector2         m_keyPosDEL;
    private Vector2         m_keyPosENTER;
    public  Transform       m_buttonTouch;
    public  Transform       m_buttonTouchENTER;
    private alphaPulseImage m_alphaPulseAction;
	
	void Start () 
    {
        m_word          = new StringBuilder(MAX_WORD_SIZE, MAX_WORD_SIZE);
        m_keysLocked    = false;
        m_keyPos        = new Vector2[NUMB_INPUTKEYS];
        char curLetter  = 'A';

        for (int i =0; i<NUMB_INPUTKEYS; i++)
        {
            m_keyPos[i] = GameObject.Find("Key_" + curLetter).GetComponent<Transform>().position;
            curLetter++;
        }

        m_keyPosDEL         = GameObject.Find("Key_DEL").GetComponent<Transform>().position;
        m_keyPosENTER       = GameObject.Find("Key_ENTER").GetComponent<Transform>().position;

        m_alphaPulseAction  = new alphaPulseImage();
        m_alphaPulseAction.forceDone();
        


	}

    public void registerForKeyboardEvent(keyboardEventHandler keyboardEventDelegate)
    {
        keyboardEvent += keyboardEventDelegate;
    }


    public void deleteLetter()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.m_typeSound, false, 1);
        
        showButtonTouch(m_keyPosDEL, false);
        
        if (m_word.Length > 0)
        {
            m_word.Remove(m_word.Length - 1, 1);
        }
        keyboardEvent(KeyboardEvent.KE_WORDUPDATE, m_word.ToString());
 
    }

    public void enterKey()
    {
        SoundManager.instance.PlaySound(SoundManager.instance.m_typeSound, false, 1);

        showButtonTouch(m_keyPosENTER, true);
                
        keyboardEvent(KeyboardEvent.KE_SUBMITWORD, null);

    }
        
    public void lockKeyboard(bool doLock)
    {
        m_keysLocked = doLock;
    }

    public void clearInputWord()
    {
        m_word.Length = 0;
        keyboardEvent(KeyboardEvent.KE_WORDUPDATE, m_word.ToString());
        
    }

    public void inputLetter(string letter)
    {
        showButtonTouch(m_keyPos[(int)letter[0] - LETTER_TO_ARRAY_OFFSET], false);
        SoundManager.instance.PlaySound(SoundManager.instance.m_typeSound, false, 1);

        if (m_keysLocked) return;

        if (m_word.Length < MAX_WORD_SIZE)
        {
            m_word.Append(letter);
        }
        keyboardEvent(KeyboardEvent.KE_WORDUPDATE, m_word.ToString());
        
    }

    private void showButtonTouch(Vector2 buttonPos, bool isEnterKey)
    {
        if (!isEnterKey)
        {
            m_buttonTouch.position = buttonPos;
            m_alphaPulseAction.setup(m_buttonTouch.GetComponent<Image>(), 10f, 15f, 0, 0, 1, 1, false);
        }
        else
        {
            m_buttonTouchENTER.position = buttonPos;
            m_alphaPulseAction.setup(m_buttonTouchENTER.GetComponent<Image>(), 10f, 15f, 0, 0, 1, 1, false);
        }
    }

    

    void Update()
    {
        if (!m_alphaPulseAction.isDone())
        {
            m_alphaPulseAction.update();
        }
    }
}
