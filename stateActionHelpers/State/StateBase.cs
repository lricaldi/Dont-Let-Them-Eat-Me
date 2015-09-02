using UnityEngine;
using System.Collections;

public class StateBase <T>
{
    public enum StateStep { SSInit, SSRuning, SSEnd };

    protected T         m_refObj;
    public StateStep    curStep;

    public StateBase()
    {
        resetState();
    }

    public StateBase(T refObj)
    {
        m_refObj = refObj;
        resetState();
    }

	public virtual void resetState()
	{
		curStep = StateStep.SSInit;
	}

	public virtual void initState()
	{
		//Debug.Log ("initBase ");
	}

	public virtual void runState(float delta)
	{
		//Debug.Log ("runBase ");
	}

	public virtual void endState()
	{
		//Debug.Log ("endBase ");
	}

	public void updateState(float delta)
	{
		switch (curStep)
		{
        case StateStep.SSRuning:
            runState(delta);
            break;
		case StateStep.SSInit:
			initState();
			break;
		case StateStep.SSEnd:
			endState();
			break;
		}
	}


}
