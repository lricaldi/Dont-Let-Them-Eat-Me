using UnityEngine;
using System.Collections;

public class EnemyFallNormalItemState  : StateBaseWithActions<AttackItem>
{
    private enum ActionEnum { AE_GODOWN, AE_HIT, AE_BOUNCEFALL, AE_Length }

    private bool m_targetAny;

    public EnemyFallNormalItemState(AttackItem refItem):base(refItem)
    {
        m_actions                                   = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_GODOWN]        = new movAtoB();
        m_actions[(int)ActionEnum.AE_HIT]           = new waitTime(0.05f);
        m_actions[(int)ActionEnum.AE_BOUNCEFALL]    = new movFall();
    }

    public override void initState()
    {
        
        // Find an enemy to hit we need the object
        m_refObj.EnemyToKill = SceneManager.instance.getEnemyManager().getNextEnemyToKill(m_refObj.getType());
        Vector2 endPos;
        Vector2 startPos;
        m_targetAny = false;
        

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
        
        Vector2 startSpeed = new Vector2(Random.Range(-0.25f, 0.25f), 1f).normalized * 8;
        ((movFall)m_actions[(int)ActionEnum.AE_BOUNCEFALL]).setup(m_refObj.GetComponent<Transform>(), startSpeed,
                                                                -20f, ViewManager.instance.getBottomScreenY() - 1,
                                                                Random.Range(100, 150));

        m_curAction = (int)ActionEnum.AE_GODOWN;
        curStep     = StateStep.SSRuning;
    }

    public override void runState()
    {
       if(m_curAction == (int)ActionEnum.AE_GODOWN)
       {
            // if attack item collides with enemy (since its vertical move this will hit the enemy even when moving up vertically).
            if (!m_refObj.EnemyToKill.isOnPath())
            {
                m_targetAny = true;
            }

            Enemy collidedEnemy = m_refObj.CollidedEnemy;
            if (collidedEnemy != null)
            {
                if(m_targetAny || collidedEnemy.GetInstanceID() == m_refObj.EnemyToKill.GetInstanceID())
                {
                    //m_refObj.EnemyToKill.kill();
                    collidedEnemy.kill(m_refObj.getType(), true);
                    m_refObj.EnemyToKill = null;
                    m_actions[m_curAction].forceDone();
                }
            }
            // if enemy has moved in the x axis and is not in line for the attack (when enemy jumps on target) then we use the next enemy in line to attack.
       }
        base.runState();
    }
    protected override bool actionDone()
    {
        if( m_curAction == (int)ActionEnum.AE_GODOWN )
        {
            SoundManager.instance.PlaySound(SoundManager.instance.m_hitEnemy, false, 1);
            m_refObj.enableCollider2D(false);
        }
        return base.actionDone();
    }

    public override void endState()
    {
        m_refObj.enableCollider2D(false);
        m_refObj.doneAttacking();
        resetState();
    }
}
