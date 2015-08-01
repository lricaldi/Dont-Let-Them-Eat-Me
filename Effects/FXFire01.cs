using UnityEngine;
using System.Collections;

public class FXFire01 : FXBase 
{
	public override void triggerEffect()
	{
		GetComponent<Animator>().SetTrigger("startEffect");
	}

    public override void endEffect()
	{
        //Debug.Log("endEffect");
		GetComponent<Animator>().ResetTrigger("endEffect");
		EffectsManager.instance.freeEffect(this);
	}
}

