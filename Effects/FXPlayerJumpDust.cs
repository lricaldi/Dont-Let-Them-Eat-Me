using UnityEngine;
using System.Collections;

public class FXPlayerJumpDust : FXBase {

	private const float MAX_DIST  = 2f;
	private const float MAX_SPEED = 3f;
	
	private FXBase	m_rightDust;
	private FXBase	m_leftDust;
	private float	m_distance;
	private bool	m_effectTriggered = false;
	
	
	
	public override void triggerEffect()
	{
		Vector2 curPos = GetComponent<Transform>().position;
		m_rightDust 	= EffectsManager.instance.getEffect(curPos, Quaternion.identity, EffectsManager.FXType.FXT_Dust01);	
		m_leftDust 		= EffectsManager.instance.getEffect(curPos, Quaternion.identity, EffectsManager.FXType.FXT_Dust01);	
		
		m_rightDust.triggerEffect();
		m_leftDust.triggerEffect();
		
		m_effectTriggered 	= true;
		m_distance 			= 0;
		
	}
	
	void Update()
	{
	
		if(m_effectTriggered)
		{
			
			Vector2 rightPos = m_rightDust.GetComponent<Transform>().position;
			rightPos.x += MAX_SPEED *  Time.deltaTime;
			m_rightDust.GetComponent<Transform>().position = rightPos;
			
			Vector2 leftPos = m_leftDust.GetComponent<Transform>().position;
			leftPos.x -= MAX_SPEED *  Time.deltaTime;
			m_leftDust.GetComponent<Transform>().position = leftPos;
			
			m_distance += MAX_SPEED *  Time.deltaTime;
			
			if(m_distance > MAX_DIST)
			{
				
				endEffect();
			}
		}
		
	}
	
	private void endEffect()
	{
	
		/*m_rightDust.GetComponent<Transform>().position = Vector2.zero;
		m_leftDust.GetComponent<Transform>().position = Vector2.zero;
		m_rightDust.triggerEffect();
		m_leftDust.triggerEffect();*/
		
		m_distance = 0;
		m_effectTriggered = false;
		
		EffectsManager.instance.freeEffect(m_rightDust);
		EffectsManager.instance.freeEffect(m_leftDust);
		EffectsManager.instance.freeEffect(this);
	}
	
}
