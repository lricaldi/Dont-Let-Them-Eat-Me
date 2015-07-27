using UnityEngine;
using System.Collections;

public class FXExplosion01 : FXBase 
{
	public override void triggerEffect()
	{
		GetComponent<Animator>().SetTrigger("startEffect");
        //SoundManager.instance.playEffectAudio((int)EffectsAudio.EffectSound.ES_Explosion02);
	}
	
	public void effectFinishedB()
	{
		GetComponent<Animator>().ResetTrigger("startEffect");
		EffectsManager.instance.freeEffect(this);
	}
}
