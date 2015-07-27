using UnityEngine;
using System.Collections;

public class FXDust02 : FXBase
{
	public override void triggerEffect()
	{
		GetComponent<Animator>().SetTrigger("startEffect");
	}
	
	public void effectFinished()
	{
		GetComponent<Animator>().ResetTrigger("startEffect");
		EffectsManager.instance.freeEffect(this);
	}
}
