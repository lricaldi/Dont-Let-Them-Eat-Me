using UnityEngine;
using System.Collections;

public class runConversation : StateActionBase
{

    /*private TalkBox m_talkBox;
    private bool m_conversationRunning;
    TalkBoxConversation[] m_conversations;


    public runConversation(TalkBoxConversation[] conversations)
    {
        m_conversations = conversations;
        m_conversationRunning = false;
        m_talkBox = GameObject.Find("TalkBox").gameObject.GetComponent<TalkBox>();
        m_talkBox.setConvesations(conversations);
    }

    public override void update() 
    {
        if (!m_conversationRunning)
        {
            m_talkBox.runConversations();
            m_conversationRunning = true;
    
        }
        else
        {
            if (!m_talkBox.isConvRunning())
            {
                m_talkBox.hideElements();
                m_done = true;
            }
        }
    }



    public override void reset()
    {
        base.reset();
    }*/
}
