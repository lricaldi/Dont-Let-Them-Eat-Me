using UnityEngine;
using System.Collections;

public class ItemBeltDefaultState : StateBase<ItemsBelt>
{
    private const float MOVEMENT_SPEED = 4;
    private const float TOUCH_ITEM_HINT_TIME = 5;

    private Color m_darkRed;

    private enum ItemDirectionMove { IDM_LEFT, IDM_RIGHT, IDM_NONE }
    
    private ItemDirectionMove   m_beltMovingDirection;
    private GroupActions        m_moveGroup;
    private movAtoB[]           m_moveActions;
    private bool                m_wordMatch;
    private float               m_timer;
    
    

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
        m_wordMatch = false;
        m_timer = 0;

        m_darkRed = new Color(0.59f,0,0);
    }

    public  override void initState()
    {
        m_refObj.setUpdateTime(0.03f);
        setupLayerOrder();
        curStep = StateStep.SSRuning;
    }

    public bool hasWordMatch()
    {
        return m_wordMatch;
    }

    public  override void runState(float delta)
	{
        
        if (m_wordMatch )
        {
            m_timer += delta;
            if (m_timer > TOUCH_ITEM_HINT_TIME)
            {
                SceneManager.instance.getUIScript().showShotTouch();
                m_timer = 0;
            }
        }
        if (!isFocusItemInCenterPos() && m_beltMovingDirection == ItemDirectionMove.IDM_NONE)
        {
            
            m_beltMovingDirection = findFocusItemNextMove();
            moveItems();
        }
        else if (m_beltMovingDirection != ItemDirectionMove.IDM_NONE)
        {
            m_moveGroup.update(delta);
            if (m_moveGroup.isDone())
            {
                m_moveGroup.reset();
                m_beltMovingDirection = ItemDirectionMove.IDM_NONE;
                
            }

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

    private void setupLayerOrder()
    {
        for (int i = 0; i < ItemsBelt.NUM_OF_ITEMS; i++)
        {
    
            m_refObj.Items[i].ItemView.GetComponent<SpriteRenderer>().sortingOrder = m_refObj.LayerOrders[m_refObj.Items[i].Position] + 1;
           /* if (m_refObj.Items[i].ItemView.getEffectBGTransform() != null)
            {
                m_refObj.Items[i].ItemView.getEffectBGTransform().GetComponent<SpriteRenderer>().sortingOrder = m_refObj.LayerOrders[m_refObj.Items[i].Position] - 1;
            }*/


        }
    }

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
            m_refObj.Items[i].ItemView.GetComponent<SpriteRenderer>().sortingOrder  = m_refObj.LayerOrders[nextPos]+1;
            /*if (m_refObj.Items[i].ItemView.getEffectBGTransform() != null)
            {
                m_refObj.Items[i].ItemView.getEffectBGTransform().GetComponent<SpriteRenderer>().sortingOrder = m_refObj.LayerOrders[nextPos] - 1;
            }*/

            
        }
    }

    private int getNextPos(int curPos, int moveValue)
    {
        int nextPos = curPos + moveValue;
        
        if (nextPos < 0)                        nextPos = ItemsBelt.NUM_OF_ITEMS - 1;
        if (nextPos >= ItemsBelt.NUM_OF_ITEMS)  nextPos = 0;

        return nextPos;

    }

    /*private bool itemsAreMoving()
    {
        return !m_moveGroup.isDone();
    }*/
    
    public void wordUpdated(string newWord)
    {
        bool containsSubString = true;
        if (!m_refObj.Items[m_refObj.FocusItem].ItemName.StartsWith(newWord))
        {
            containsSubString = false;
            for (int i = 0; i < ItemsBelt.NUM_OF_ITEMS; i++)
            {
                if (m_refObj.Items[i].ItemName.StartsWith(newWord))
                {
                    containsSubString = true;
                    m_refObj.FocusItem = i;
                    break;
                }
            }
         }

        SceneManager.instance.getUIScript().updateInputWordColor((containsSubString) ? Color.white : m_darkRed);
        m_wordMatch = m_refObj.InputWord == m_refObj.Items[m_refObj.FocusItem].ItemName;
        m_timer = 0;
        m_refObj.ItemReady.SetActive(m_wordMatch);
        
            
        
    }

    public void trySubmitWord(string newWord)
    {
        if (m_beltMovingDirection != ItemDirectionMove.IDM_NONE) return;
        if (m_refObj.InputWord == m_refObj.Items[m_refObj.FocusItem].ItemName || m_refObj.debugFireItem)
        {
            SoundManager.instance.PlaySound(SoundManager.instance.m_correctWord, false, 1);

            SceneManager.instance.lockInput(true);

            m_refObj.debugFireItem = false;
            curStep = StateStep.SSEnd;
        }
        else
        {
            SoundManager.instance.PlaySound(SoundManager.instance.m_wrongInput, false, 1);
        }
    }
}
