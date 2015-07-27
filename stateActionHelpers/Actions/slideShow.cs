using UnityEngine;
using System.Collections;


public struct slideItem
{
	public float	m_fadeInSpeed;
	public float m_fadeOutSpeed;
	public GameObject m_image;
	public float m_showTime;

	public slideItem(float fadeInSpeed, float fadeOutSpeed, GameObject img, float showTime)
	{
		m_fadeInSpeed	= fadeInSpeed;
		m_fadeOutSpeed	= fadeOutSpeed;
		m_image			= img;
		m_showTime		= showTime;

	}

}
public class slideShow : StateActionBase
{
	/*public enum SSAction { SS_SHOWIMAGE, SS_FADEIN, SS_WAIT, SS_FADEOUT, SS_HIDEIMAGE, SS_Length }
	private StateActionBase[] m_actions;

	private int m_curAction;

	private slideItem[] m_slides;
	private int m_curSlide;
	
	private float m_curSlideTimer;
	private bool m_waitingOnSlide;

	public override void reset()
	{
 		 base.reset();
		m_curSlide = 0;
		m_curSlideTimer = 0;
		m_waitingOnSlide = false;
		for (int i = 0; i < m_slides.Length; i++ )
		{
			m_slides[i].m_image.SetActive(false);
		}
	}

	public slideShow(slideItem[] slides)
	{
		m_slides = slides;
		initActions();
		
		reset();
	}

	private void initActions()
	{
		m_actions = new StateActionBase[(int)SSAction.SS_Length];

		m_actions[(int)SSAction.SS_SHOWIMAGE]		= new ShowFullImage(true, m_slides[0].m_image);
		m_actions[(int)SSAction.SS_FADEIN]			= new BlackFade(true, m_slides[0].m_fadeInSpeed);
		m_actions[(int)SSAction.SS_WAIT]			= new waitTime(m_slides[0].m_showTime);
		m_actions[(int)SSAction.SS_FADEOUT]			= new BlackFade(true, m_slides[0].m_fadeOutSpeed);
		m_actions[(int)SSAction.SS_HIDEIMAGE]		= new ShowFullImage(false, m_slides[0].m_image);

		m_curAction = (int)SSAction.SS_SHOWIMAGE;

	}

	public void setupActions()
	{
		((ShowFullImage)m_actions[(int)SSAction.SS_SHOWIMAGE]).setup(true, m_slides[m_curSlide].m_image);
		((BlackFade)m_actions[(int)SSAction.SS_FADEIN]).setup(true, m_slides[m_curSlide].m_fadeInSpeed);
		((waitTime)m_actions[(int)SSAction.SS_WAIT]).setup(m_slides[m_curSlide].m_showTime);
		((BlackFade)m_actions[(int)SSAction.SS_FADEOUT]).setup(true, m_slides[m_curSlide].m_fadeOutSpeed);
		((ShowFullImage)m_actions[(int)SSAction.SS_HIDEIMAGE]).setup(false, m_slides[m_curSlide].m_image);
	}

	public override void update() 
	{
 		if(!m_done)
		{
			m_actions[m_curAction].update();

			if (m_actions[m_curAction].isDone())
			{
				m_actions[m_curAction].reset();
				switch((SSAction)m_curAction)
				{
					case SSAction.SS_HIDEIMAGE:
						
						++m_curSlide;
						if (m_curSlide >= m_slides.Length)
						{
							m_done = true;
						}
						else
						{
							setupActions();
							m_curAction = (int)SSAction.SS_SHOWIMAGE;
						}

					break;
					default:
						++m_curAction;
					break;
				}
			}
		}
	}*/
	
}
