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
		//Debug.Log ("resetBase ");
		curStep = StateStep.SSInit;
	}

	public virtual void initState()
	{
		//Debug.Log ("initBase ");
	}

	public virtual void runState()
	{
		//Debug.Log ("runBase ");
	}

	public virtual void endState()
	{
		//Debug.Log ("endBase ");
	}

	public void updateState()
	{
		switch (curStep)
		{
		case StateStep.SSInit:
			initState();
			break;
		case StateStep.SSRuning:
			runState();
			break;
		case StateStep.SSEnd:
			endState();
			break;
		}
	}


}
