using UnityEngine;
using System.Collections;

public class ItemBeltLaunchItemState : StateBaseWithActions<ItemsBelt>
{
    private enum ActionEnum {AE_WAITSHINE, AE_ANIMATEIN, /*AE_WAIT,*/ AE_Length }

    public ItemBeltLaunchItemState(ItemsBelt refBelt)
        : base(refBelt)
    {
        m_actions                               = new StateActionBase[(int)ActionEnum.AE_Length];
        m_actions[(int)ActionEnum.AE_WAITSHINE] = new waitTime(0.5f); 
        m_actions[(int)ActionEnum.AE_ANIMATEIN] = new movAtoB();
        //m_actions[(int)ActionEnum.AE_WAIT]      = new waitTime(0.1f); 
    }

    public override void initState()
    {
        SceneManager.instance.lockInput(true);
        setupFocusItemLaunch();

        Vector3 focusItemPos    = m_refObj.Items[m_refObj.FocusItem].GetComponent<Transform>().position;
        focusItemPos.y          = focusItemPos.y - 2;

        m_refObj.Items[m_refObj.FocusItem].GetComponent<Transform>().position = focusItemPos;
        m_refObj.Items[m_refObj.FocusItem].getNewItem();

        ((movAtoB)m_actions[(int)ActionEnum.AE_ANIMATEIN]).setup(m_refObj.Items[m_refObj.FocusItem].gameObject, 
                                                                    m_refObj.Items[m_refObj.FocusItem].GetComponent<Transform>().position,
                                                                    m_refObj.Positions[ItemsBelt.FOCUS_POS_INDEX].position, 3, 0);

        m_curAction = (int)ActionEnum.AE_WAITSHINE;
        curStep     = StateStep.SSRuning;
    }
   
    private void setupFocusItemLaunch()
    {

        //AttackItem attkItem = SceneManager.instance.getAttackItem();
        AttackItem attkItem = InstanceFactory.instance.getAttackItem(m_refObj.Positions[ItemsBelt.FOCUS_POS_INDEX].position, Quaternion.identity);
        attkItem.setup(m_refObj.Positions[ItemsBelt.FOCUS_POS_INDEX].position,
                        m_refObj.Items[m_refObj.FocusItem].ItemView,
                        m_refObj.Items[m_refObj.FocusItem].ItemEffect);

        attkItem.fireItem();
    }

    public override void endState()
    {
        SceneManager.instance.clearInputWord();
        SceneManager.instance.lockInput(false);

        m_refObj.setCurState((int)ItemsBelt.StateEnum.SE_DEFAULT);
        resetState();
    }
}
