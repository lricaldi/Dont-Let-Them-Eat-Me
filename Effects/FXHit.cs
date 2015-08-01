using UnityEngine;
using System.Collections;

public class FXHit : FXBase 
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
