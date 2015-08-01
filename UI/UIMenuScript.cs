using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMenuScript : MonoBehaviour 
{
    private wobble m_wobbleStart;


    void Start()
    {
        SoundManager.instance.PlayMusic(SoundManager.instance.m_musicTitle, true);
        m_wobbleStart = new wobble(GameObject.Find("Start").GetComponent<Transform>(), 10, 2);
    }

    void Update()
    {
        m_wobbleStart.update();
    }

    public void pressStartButton()
    {
        Debug.Log("Start button pressed");
        Application.LoadLevel("Game");
    }

}
