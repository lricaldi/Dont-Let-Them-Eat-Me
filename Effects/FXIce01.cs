using UnityEngine;
using System.Collections;

public class FXIce01 : FXBase
{
    public override void triggerEffect()
    {
        GetComponent<Animator>().SetTrigger("startEffect");
    }

    public override void endEffect()
    {
        GetComponent<Animator>().ResetTrigger("endEffect");
        EffectsManager.instance.freeEffect(this);
    }
}
