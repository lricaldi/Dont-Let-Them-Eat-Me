using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemiesCounter : MonoBehaviour 
{
    private Text m_counterText;
    private int m_count;

    public void setCounter(int enemyCount)
    {
        m_count = enemyCount;
        if (m_counterText == null)
        {
            m_counterText = GetComponent<Transform>().Find("enemyCount").GetComponent<Text>();
        }
        m_counterText.text = "" + enemyCount;
    }

    public void updateCounter(int value)
    {
        m_count += value;
        m_counterText.text = "" + m_count;
    }
}
