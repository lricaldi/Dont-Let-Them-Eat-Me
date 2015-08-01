using UnityEngine;
using System.Collections;

public class AttackItem : ObjectWithStates<AttackItem>
{

    public enum StateEnum { SE_SHINE, SE_LAUNCH, SE_FALL_ON_TARGET, SE_FALL_NORMAL, SE_FALL_HEAVY, SE_Length }

    private ItemView                m_itemView;
    private BeltItem.EffectTypeEnum m_effectType;
    private Vector3                 m_startPos;
    private CircleCollider2D        m_circleCollider;
    private bool                    m_itemFired;
    private Enemy                   m_enemyToKill;
    private Enemy                   m_lastCollidedEnemy;
    private GameObject              m_launchEffect;

    public Enemy EnemyToKill
    {
        get
        {
            return this.m_enemyToKill;
        }
        set
        {
            this.m_enemyToKill = value;
        }
    }

    public Enemy CollidedEnemy
    {
        get
        {
            Enemy tempEnemy     = m_lastCollidedEnemy;
            m_lastCollidedEnemy = null;
            return tempEnemy;
        }
       
    }

    protected override void init()
    {
        m_launchEffect      = GetComponent<Transform>().Find("CorrectWordEffect").gameObject;
        m_circleCollider    = GetComponent<CircleCollider2D>();
        enableCollider2D(false);
        
    }

    public void setup(Vector3 startPos, ItemView itemView, BeltItem.EffectTypeEnum effectType)
    {
        m_startPos                          = startPos;
        GetComponent<Transform>().position  = startPos;

        m_itemView                          = itemView;
        
        itemView.GetComponent<Transform>().SetParent(this.GetComponent<Transform>());
        itemView.GetComponent<Transform>().localPosition = Vector3.zero;
       

        m_effectType    = effectType;
        m_itemFired     = false;
    }

    protected override void initStates()
    {
        m_states                                    = new StateBase<AttackItem>[(int)StateEnum.SE_Length];
        m_states[(int)StateEnum.SE_SHINE]           = new ShineItemState(this);
        m_states[(int)StateEnum.SE_LAUNCH]          = new LaunchItemState(this);
        m_states[(int)StateEnum.SE_FALL_NORMAL]     = new EnemyFallNormalItemState(this);
        m_states[(int)StateEnum.SE_FALL_ON_TARGET]  = new TargetFallItemState(this);
        m_states[(int)StateEnum.SE_FALL_HEAVY]      = new EnemyFallHeavyItemState(this);

        m_curState                                  = (int) StateEnum.SE_SHINE;
    }

    protected override void update()
    {
        if (m_itemFired && m_states !=null)
        {
            base.update();
        }
    }

    public GameObject getLaunchEffect()
    {
        return m_launchEffect;
    }

    public void fireItem()
    {
        m_curState  = (int)StateEnum.SE_SHINE;
        m_itemFired = true;
    }

    public void setLayer(string layerName, int order)
    {
        m_itemView.GetComponent<SpriteRenderer>().sortingLayerName = layerName;
        m_itemView.GetComponent<SpriteRenderer>().sortingOrder = order;
    }

    public bool itemAvailable()
    {
        return !m_itemFired;
    }

    public void doneAttacking()
    {
        m_itemView.GetComponent<Transform>().SetParent(null);
        InstanceFactory.instance.freeItemView(m_itemView);
        m_itemFired = false;
        InstanceFactory.instance.freeAttackItem(this);
    }

    public void enableCollider2D(bool doEnable)
    {
        m_circleCollider.enabled = doEnable;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Enemy>() != null)
        {
            m_lastCollidedEnemy = other.GetComponent<Enemy>();
        }
    }

    public BeltItem.EffectTypeEnum getType()
    {
        return m_itemView.m_effectType;
    }
}
