using UnityEngine;
using System.Collections;

public class ItemsBelt : ObjectWithStates<ItemsBelt> {

    public const int    NUM_OF_ITEMS = 7;
    public  const int   FOCUS_POS_INDEX = 0;

    public enum StateEnum { SE_DEFAULT, SE_LAUNCHITEM, SE_Length}

    public  BeltItem[]  m_items;
    public  Transform[] m_positions;
    private Vector2[]   m_scales;
    private int[]       m_layerOrderAtPos;
    private int         m_focusItem;
    private string      m_inputWord;

    public bool         debugFireItem;
    
    public BeltItem[] Items
    {
        get
        {
            return this.m_items;
        }
    }

    public Transform[] Positions
    {
        get
        {
            return this.m_positions;
        }
    }
    
    public Vector2[] Scales
    {
        get
        {
            return this.m_scales;
        }
    }

    public int[] LayerOrders
    {
        get
        {
            return this.m_layerOrderAtPos;
        }
    }

    public int FocusItem
    {
        get
        {
            return this.m_focusItem;
        }
        set
        {
            this.m_focusItem = value;
        }
    }

    public string InputWord
    {
        get
        {
            return this.m_inputWord;
        }
        set
        {
            this.m_inputWord = value;
        }
    }

    protected override void init () 
    {
        m_scales            = new Vector2[] { Vector2.one, new Vector2(0.95f, 0.95f), new Vector2(0.9f, 0.9f), new Vector2(0.85f, 0.85f),
                                                new Vector2(0.85f, 0.85f), new Vector2(0.9f,0.9f), new Vector2(0.95f,0.95f) };
        m_layerOrderAtPos   = new int   [] {4,3,2,1,1,2,3};
        m_inputWord         = "";
        m_focusItem         = FOCUS_POS_INDEX;

        debugFireItem       = false;

        setupItems();
        registerToKeyboard();
    }

    protected override void initStates()
    {
        m_states                                = new StateBase<ItemsBelt>[(int)StateEnum.SE_Length];
        m_states[(int)StateEnum.SE_DEFAULT]     = new ItemBeltDefaultState(this);
        m_states[(int)StateEnum.SE_LAUNCHITEM]  = new ItemBeltLaunchItemState(this);

        m_curState                              = (int)StateEnum.SE_DEFAULT;
    }

    private void registerToKeyboard()
    {
         GameKeyboard keyboard  = GameObject.Find("GameKeyboard").GetComponent<GameKeyboard>();
         keyboard.wordUpdate    += new GameKeyboard.wordUpdatedEventHandler(wordUpdated);
    }

    private void wordUpdated(string newWord)
    {
        m_inputWord = newWord;
        ((ItemBeltDefaultState)m_states[(int)StateEnum.SE_DEFAULT]).wordUpdated(newWord);
    }

    private void setupItems()
    {
        for(int i = 0; i < m_items.Length; i++)
        {
            m_items[i].Position                             = i;
            m_items[i].GetComponent<Transform>().localScale = m_scales[i];
            m_items[i].GetComponent<Transform>().position   = m_positions[i].position;
        }
    }
}
