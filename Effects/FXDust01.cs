using UnityEngine;
using System.Collections;

public class FXDust01 : FXBase
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
