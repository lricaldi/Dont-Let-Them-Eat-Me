using UnityEngine;
using System.Collections;

public class BeltItem : MonoBehaviour 
{
    public  enum EffectTypeEnum {ETE_NORMAL, ETE_FIRE, ETE_ICE, ETE_HEAVY, ETE_SHOCK, ETE_Length}

    private int             m_position;
    private string          m_itemName;
    private GameObject      m_nameObj;
    private ItemView        m_itemView;
    private EffectTypeEnum  m_effectType;

    public int Position
    {
        get
        {
            return this.m_position;
        }
        set
        {
            this.m_position = value;
        }
    }

    public string ItemName
    {
        get
        {
            return this.m_itemName;
        }
        set
        {
            this.m_itemName = value;
        }
    }

    public ItemView ItemView
    {
        get
        {
            return this.m_itemView;
        }
    }

    public EffectTypeEnum ItemEffect
    {
        get
        {
            return this.m_effectType;
        }
    }

   	void Start () 
    {
        getNewItem();
   }

    public void getNewItem()
    {
        m_itemView      = InstanceFactory.instance.getRandomItemView(GetComponent<Transform>().position, Quaternion.identity);
        m_itemView.GetComponent<Transform>().SetParent(this.GetComponent<Transform>());
        m_itemView.GetComponent<Transform>().localScale = Vector3.one;

        m_itemName      = m_itemView.m_itemName;
        m_effectType    = m_itemView.m_effectType;
    }

}
