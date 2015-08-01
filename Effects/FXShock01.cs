using UnityEngine;
using System.Collections;

public class FXShock01 : FXBase
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
