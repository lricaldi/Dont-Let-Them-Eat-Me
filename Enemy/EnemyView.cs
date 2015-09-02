using UnityEngine;
using System.Collections;

public class EnemyView : MonoBehaviour 
{
    //public enum EnemyTypeEnum { ETE_FIRE, ETE_METAL, ETE_WATER, ETE_ROCK, ETE_Length }

    public BeltItem.EffectTypeEnum m_type;
    private GameObject m_effectBG;

    public void showEffectBG(bool doShow)
    {
        if (m_effectBG == null)
        {
            m_effectBG = GetComponent<Transform>().Find("effectBG").gameObject;
        }
        m_effectBG.SetActive(doShow);
    }

}
