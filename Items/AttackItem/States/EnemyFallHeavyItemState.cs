using UnityEngine;
using System.Collections;

public class EnemyFallHeavyItemState : StateBaseWithActions<AttackItem>
{
    private enum ActionEnum { AE_GODOWN, AE_HIT, AE_BOUNCEFALL, AE_Length }

    //private bool m_targetAny;

    public EnemyFallHeavyItemState(AttackItem refItem)
        : base(refItem)
    {
        m_actions                                   = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_GODOWN]        = new movAtoB();
        m_actions[(int)ActionEnum.AE_HIT]           = new waitTime(0.05f);
        m_actions[(int)ActionEnum.AE_BOUNCEFALL]    = new movFall();
    }

    public override void initState()
    {
        Debug.Log("fall Heavy init");
        // Find an enemy to hit we need the object
        m_refObj.EnemyToKill = SceneManager.instance.getEnemyManager().getNextEnemyToKill();
        Vector2 endPos;
        Vector2 startPos;
                

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

           if(m_refObj.GetComponent<Transform>().position.y < ViewManager.instance.getBottomScreenY())
           {
               m_actions[m_curAction].forceDone();
           }
           
            Enemy collidedEnemy = m_refObj.CollidedEnemy;
            if (collidedEnemy != null)
            {
                SoundManager.instance.PlaySound(SoundManager.instance.m_hitEnemy, false, 1);
                collidedEnemy.kill(m_refObj.getType(), true);
            }
            // if enemy has moved in the x axis and is not in line for the attack (when enemy jumps on target) then we use the next enemy in line to attack.
       }
        base.runState();
    }
    protected override void actionDone()
    {
        if( m_curAction == (int)ActionEnum.AE_GODOWN )
        {
            m_refObj.enableCollider2D(false);
            if (m_refObj.EnemyToKill == null)
            {
                curStep = StateStep.SSRuning;
                return;
            }
        }
        

        base.actionDone();
    }

    public override void endState()
    {
        m_refObj.enableCollider2D(false);
        m_refObj.doneAttacking();
        resetState();
    }
}
