using UnityEngine;
using System.Collections;

public class EnemyFallHeavyItemState : StateBaseWithActions<AttackItem>
{
    private enum ActionEnum { AE_GODOWN, AE_HIT, AE_Length }

    private int prevNodeRowHit;

    public EnemyFallHeavyItemState(AttackItem refItem)
        : base(refItem)
    {
        m_actions                                   = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_GODOWN]        = new movAtoB();
        m_actions[(int)ActionEnum.AE_HIT]           = new waitTime(0.2f);
      
    }

    public override void initState()
    {
        // Find an enemy to hit we need the object
        m_refObj.setLayer("default", 3);
        m_refObj.EnemyToKill = SceneManager.instance.getEnemyManager().getNextEnemyToKill(m_refObj.getEffectType());
        Vector2 endPos;
        Vector2 startPos;

        prevNodeRowHit = -1;

        m_refObj.enableCollider2D(true);

        // if null we just drop item on any column.
        if (m_refObj.EnemyToKill == null)
        {
            endPos      = m_refObj.GetComponent<Transform>().position;
            endPos.y    = ViewManager.instance.getBottomScreenY();

            startPos    = m_refObj.GetComponent<Transform>().position;
            
            
        }
        else
        { // use its position to align the attack object to the colunm its in
            startPos    = m_refObj.GetComponent<Transform>().position;
            startPos.x  = m_refObj.EnemyToKill.GetComponent<Transform>().position.x;
            endPos      = startPos;
            endPos.y    = ViewManager.instance.getBottomScreenY();
        }

        ((movAtoB)m_actions[(int)ActionEnum.AE_GODOWN]).setup(m_refObj.gameObject, startPos, endPos, 0.5f, 0);
        
        //Vector2 startSpeed = new Vector2(Random.Range(-0.25f, 0.25f), 1f).normalized * 8;
        //((movFall)m_actions[(int)ActionEnum.AE_BOUNCEFALL]).setup(m_refObj.GetComponent<Transform>(), startSpeed,
                                                                //-20f, ViewManager.instance.getBottomScreenY() - 1,
                                                                //Random.Range(100, 150));

        m_curAction = (int)ActionEnum.AE_GODOWN;
        curStep     = StateStep.SSRuning;
    }

    public override void runState(float delta)
    {
       if(m_curAction == (int)ActionEnum.AE_GODOWN)
       {
            Enemy collidedEnemy = m_refObj.CollidedEnemy;

            if(m_refObj.GetComponent<Transform>().position.y < ViewManager.instance.getBottomScreenY())
            {
                m_actions[m_curAction].forceDone();
            }
            else if (collidedEnemy != null && collidedEnemy.getNodeRow() != prevNodeRowHit && !collidedEnemy.isDead())
            {
                prevNodeRowHit = collidedEnemy.getNodeRow();
                SoundManager.instance.PlaySound(SoundManager.instance.m_hitEnemy, false, 1);
                collidedEnemy.kill(m_refObj.getEffectType(), true);
                m_actions[m_curAction].forceDone();
            }
            // if enemy has moved in the x axis and is not in line for the attack (when enemy jumps on target) then we use the next enemy in line to attack.
       }
       base.runState(delta);
    }
    protected override bool actionDone()
    {
        if (m_curAction == (int)ActionEnum.AE_GODOWN)
        {
            if (m_refObj.GetComponent<Transform>().position.y <= ViewManager.instance.getBottomScreenY())
            {
                curStep = StateStep.SSEnd;
            }
            else
            {
                return base.actionDone();
            }
        }
        else if (m_curAction == (int)ActionEnum.AE_HIT)
        {
            m_curAction = (int)ActionEnum.AE_GODOWN;
        }
        else 
        {
            return base.actionDone();
        }

        return false;
        
    }

    public override void endState()
    {
        m_refObj.enableCollider2D(false);
        m_refObj.doneAttacking();
        resetState();
    }
}
