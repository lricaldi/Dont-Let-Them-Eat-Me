using UnityEngine;
using System.Collections;

public class EnemyEatingState : StateBaseWithActions<Enemy>
{
    private enum ActionEnum { AE_ANIMATE, AE_WAIT, AE_JUMPTOEAT, AE_EAT, AE_Length }

    private bool m_tryToAttach;
    private float m_rotation;
    private float m_curRotation;
    

    public EnemyEatingState(Enemy refEnemy) :base(refEnemy)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null, SceneManager.instance.hashIDs.eat, m_refObj.getViewAnimator()); 
        m_actions[(int)ActionEnum.AE_WAIT]      = new waitTime(1);
        m_actions[(int)ActionEnum.AE_JUMPTOEAT] = new movArc();
        m_actions[(int)ActionEnum.AE_EAT]       = new waitTime(0); // wait until enemy gets hit.
    }

    public override void initState()
    {
       
        m_refObj.setUpdateSpeed(0.03f);
        ((movArc)m_actions[(int)ActionEnum.AE_JUMPTOEAT]).setup(m_refObj.gameObject, m_refObj.GetComponent<Transform>().position,
                                                                ViewManager.instance.getTargetPos().position, 6f, 0, -0.01f);

        m_rotation      = Random.Range(-0.5f, 0.5f);
        m_curRotation   = 0;
        m_tryToAttach   = true;

        m_curAction     = (int)ActionEnum.AE_ANIMATE;
        curStep         = StateStep.SSRuning;
    }


    protected override bool actionDone()
    {
        if (m_curAction == (int)ActionEnum.AE_WAIT)
        {
            m_refObj.getView().showEffectBG(false);
            m_refObj.freeNode();
            SceneManager.instance.getEnemyManager().removeFromPath(m_refObj);

            SoundManager.instance.PlaySound(SoundManager.instance.m_enemyJump, false, 1);
        }
        return base.actionDone();
    }
    
    public override void runState(float delta)
    {

        if (m_curAction == (int)ActionEnum.AE_JUMPTOEAT && m_refObj.GetComponent<Transform>().position.y < ViewManager.instance.getBottomScreenY())
        {
            m_actions[m_curAction].forceEndAction();
            curStep = StateStep.SSEnd;
            return;
            
        }

        else if (m_curAction == (int)ActionEnum.AE_JUMPTOEAT && m_tryToAttach)
        {
           
            m_curRotation += m_rotation;
            m_refObj.getView().GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, m_curRotation);

            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_refObj.GetComponent<Transform>().position, 0.5f);
            bool foundTargetCollision = false;
            
            foreach(Collider2D col in colliders)
            {
                if (col.tag == "targetTag")
                {
                    foundTargetCollision = true;
                    break;
                }
            }
            if (foundTargetCollision)
            {
                m_tryToAttach = false;
                Transform attachTrans = SceneManager.instance.getEnemyTarget().attachEnemy(m_refObj);
                if (attachTrans != null)
                {
                    m_refObj.GetComponent<Transform>().position = attachTrans.position;
                    m_refObj.getView().GetComponent<Transform>().rotation = attachTrans.rotation;
                    m_refObj.getView().GetComponent<Transform>().localScale = new Vector3(0.75f, 0.75f, 1);
                  

                    m_actions[m_curAction].forceEndAction();
                }
                else
                {
                    m_refObj.kill(BeltItem.EffectTypeEnum.ETE_NORMAL);
                }
            }
            
        }
         
        base.runState(delta);
    }
 
    public override void endState()
    {
        m_refObj.setCurrentState(Enemy.StateEnum.SE_DEAD);
        resetState();
    }
}
