using UnityEngine;
using System.Collections;

public class FXElectricity01 : FXBase
{
	private bool m_isActive = false;
	public override void triggerEffect()
	{
		m_isActive = true;
		GetComponent<Animator>().SetBool("startEffect", true);
	}

	public void stopEffect()
	{
		m_isActive = false;
		GetComponent<Animator>().SetBool("startEffect", false);
	}

	public void endEffect()
	{
		m_isActive = false;
		GetComponent<Animator>().SetBool("startEffect", false);
		EffectsManager.instance.freeEffect(this);
	}

	public bool isActive()
	{
		return m_isActive;
	}
}
