using UnityEngine;
using System.Collections;

public class ItemBeltDefaultState : StateBase<ItemsBelt>
{
    private const float MOVEMENT_SPEED = 4;

    private enum ItemDirectionMove { IDM_LEFT, IDM_RIGHT, IDM_NONE }
    
    private ItemDirectionMove   m_beltMovingDirection;
    private GroupActions        m_moveGroup;
    private movAtoB[]           m_moveActions;
    

    public ItemBeltDefaultState(ItemsBelt refItemsBelt)
        : base(refItemsBelt)
    {
        m_moveActions           = new movAtoB[ItemsBelt.NUM_OF_ITEMS];

        for (int i = 0; i < ItemsBelt.NUM_OF_ITEMS; i++)
        {
            m_moveActions[i]    = new movAtoB(m_refObj.Items[i].gameObject, m_refObj.Positions[i].position, 
                                            m_refObj.Positions[i].position, 1, 0);
        }

        m_moveGroup             = new GroupActions(m_moveActions);
        m_beltMovingDirection   = ItemDirectionMove.IDM_NONE;
    }

    public  override void initState()
    {
        curStep = StateStep.SSRuning;
    }

    public  override void runState()
	{
        
        if (!isFocusItemInCenterPos() && m_beltMovingDirection == ItemDirectionMove.IDM_NONE)
        {

            m_beltMovingDirection = findFocusItemNextMove();
            moveItems();
        }
        else if (m_beltMovingDirection != ItemDirectionMove.IDM_NONE)
        {
            m_moveGroup.update();
            if (m_moveGroup.isDone())
            {
                m_moveGroup.reset();
                m_beltMovingDirection = ItemDirectionMove.IDM_NONE;
            }

        }
        else if (m_refObj.InputWord == m_refObj.Items[m_refObj.FocusItem].ItemName || m_refObj.debugFireItem)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.m_correctWord, false, 1);

            SceneManager.instance.lockInput(true);

            m_refObj.debugFireItem  = false;
            curStep                 = StateStep.SSEnd;
        }
    }

    // End State
    public  override void endState()
    {
        m_refObj.setCurState((int)ItemsBelt.StateEnum.SE_LAUNCHITEM); 
        resetState();
    }

    /* Helper functions*/
    /***********************************************************************************************************/
   
    private bool isFocusItemInCenterPos()
    {
        return m_refObj.Items[m_refObj.FocusItem].Position == ItemsBelt.FOCUS_POS_INDEX;

    }

    private ItemDirectionMove findFocusItemNextMove()
    {
        ItemDirectionMove returnValue = ItemDirectionMove.IDM_RIGHT;
        if (m_refObj.Items[m_refObj.FocusItem].Position > 0 && m_refObj.Items[m_refObj.FocusItem].Position <= 3)
        {
            returnValue = ItemDirectionMove.IDM_LEFT;
        }
        return returnValue;
    }

    private void moveItems()
    {
        int moveValue = (m_beltMovingDirection == ItemDirectionMove.IDM_LEFT) ? -1 : 1;

        int nextPos = 0;

        for (int i = 0; i < ItemsBelt.NUM_OF_ITEMS; i++)
        {
            nextPos = getNextPos(m_refObj.Items[i].Position, moveValue);

            m_moveActions[i].setup(m_refObj.Items[i].gameObject, m_refObj.Positions[m_refObj.Items[i].Position].position, 
                                        m_refObj.Positions[nextPos].position, MOVEMENT_SPEED, 0);

            m_refObj.Items[i].Position                                              = nextPos;
            m_refObj.Items[i].GetComponent<Transform>().localScale                  = m_refObj.Scales[nextPos];
            m_refObj.Items[i].ItemView.GetComponent<SpriteRenderer>().sortingOrder  = m_refObj.LayerOrders[nextPos];
        }
    }

    private int getNextPos(int curPos, int moveValue)
    {
        int nextPos = curPos + moveValue;
        
        if (nextPos < 0)                        nextPos = ItemsBelt.NUM_OF_ITEMS - 1;
        if (nextPos >= ItemsBelt.NUM_OF_ITEMS)  nextPos = 0;

        return nextPos;

    }

    private bool itemsAreMoving()
    {
        return !m_moveGroup.isDone();
    }
    
    public void wordUpdated(string newWord)
    {
        if (m_refObj.Items[m_refObj.FocusItem].ItemName.StartsWith(newWord)) return;
        for (int i = 0; i < ItemsBelt.NUM_OF_ITEMS; i++)
        {
            if (m_refObj.Items[i].ItemName.StartsWith(newWord))
            {
                m_refObj.FocusItem = i;
                break;
            }
        }
    }
}
