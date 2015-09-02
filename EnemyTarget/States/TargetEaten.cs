using UnityEngine;
using System.Collections;

public class TargetEaten : StateBaseWithActions<EnemyTarget>
{
    private enum ActionEnum { AE_ANIMATE, AE_FALL, AE_Length }

    public TargetEaten(EnemyTarget refTarget):base(refTarget)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_ANIMATE]   = new runAni(null, SceneManager.instance.hashIDs.dead, m_refObj.getViewAnimator()); // never ends.
        
        Vector2 startSpeed                      = new Vector2(Random.Range(-0.25f, 0.25f), 1f).normalized * 8;
        m_actions[(int)ActionEnum.AE_FALL]      = new movFall(m_refObj.getViewTransform(), startSpeed, -20f, 
                                                            ViewManager.instance.getBottomScreenY() - 1, Random.Range(100, 150));
    }

    public override void initState()
    {
        m_refObj.GetComponent<CircleCollider2D>().enabled = false;
        m_refObj.killAttached(BeltItem.EffectTypeEnum.ETE_NORMAL);
        SoundManager.instance.PlaySound(SoundManager.instance.m_targetDead, false, 0);
        m_curAction = (int)ActionEnum.AE_ANIMATE;
        curStep     = StateStep.SSRuning;
    }
   
    public override void endState()
    {
     
    }

}
