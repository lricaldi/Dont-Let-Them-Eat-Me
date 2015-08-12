using UnityEngine;
using System.Collections;

public class TargetHealth : MonoBehaviour 
{
    public GameObject[] m_hearts;
    public int m_curHeart;

    private bool m_isBeating;

    void Start()
    {
        m_curHeart = m_hearts.Length-1;
        m_isBeating = false;
    }

    public void setHealth(float healthPercent)
    {
        if (healthPercent > 0)
        {
            int newCurHeart = getNextCurHeart(healthPercent);
            if (newCurHeart != m_curHeart)
            {
                EffectsManager.instance.getEffect(m_hearts[m_curHeart].GetComponent<Transform>().position, Quaternion.identity, EffectsManager.FXType.FXT_Hit01).triggerEffect();

                if (m_isBeating)
                {
                    m_hearts[m_curHeart].GetComponent<Animator>().SetBool("heartBeat", false);
                    m_hearts[newCurHeart].GetComponent<Animator>().SetBool("heartBeat", true);
                }
                m_hearts[m_curHeart].SetActive(false);
                m_curHeart = newCurHeart;
            }
        }
        else
        {
            EffectsManager.instance.getEffect(m_hearts[m_curHeart].GetComponent<Transform>().position, Quaternion.identity, EffectsManager.FXType.FXT_Hit01).triggerEffect();
            if (m_isBeating)
            {
                m_hearts[m_curHeart].GetComponent<Animator>().SetBool("heartBeat", false);
            }
            m_hearts[m_curHeart].SetActive(false);
        }
        
       
    }

    private int getNextCurHeart(float healthPercent)
    {
        float healthPerHeart = (float)(100 / m_hearts.Length);
        return (int)Mathf.Floor(healthPercent / healthPerHeart);
    }

    public void doHeartBeat(bool doBeat)
    {
        m_isBeating = doBeat;
        m_hearts[m_curHeart].GetComponent<Animator>().SetBool("heartBeat", doBeat);
    }



}
