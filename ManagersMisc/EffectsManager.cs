using UnityEngine;
using System.Collections;

public class EffectsManager : MonoBehaviour 
{

	private FXHit				m_hit01;
    private FXFire01            m_fire01;
    private FXIce01             m_ice01;
    private FXShock01           m_shock01;
	

    public enum FXType { FXT_Hit01, FXT_Fire01, FXT_Ice01, FXT_Shock01, FXT_Length }
	
	public static EffectsManager instance = null;
	
	
	void Awake () 
    {

		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
	}

    void Start()
	{
        
		loadEffectsFromResources();
	}
	
	
	public FXBase getEffect(Vector2 position, Quaternion rotQuat, FXType type)
	{
		FXBase returnVal = null;
		switch(type)
		{
            case FXType.FXT_Hit01:
                returnVal = ObjectPoolManager.CreatePooled(m_hit01.gameObject, position, rotQuat).GetComponent<FXHit>();
                break;
            case FXType.FXT_Fire01:
                returnVal = ObjectPoolManager.CreatePooled(m_fire01.gameObject, position, rotQuat).GetComponent<FXFire01>();
                break;
            case FXType.FXT_Ice01:
                returnVal = ObjectPoolManager.CreatePooled(m_ice01.gameObject, position, rotQuat).GetComponent<FXIce01>();
                break;
            case FXType.FXT_Shock01:
                returnVal = ObjectPoolManager.CreatePooled(m_shock01.gameObject, position, rotQuat).GetComponent<FXShock01>();
                break;
        }

        returnVal.GetComponent<SpriteRenderer>().sortingOrder = 4;
        returnVal.GetComponent<SpriteRenderer>().sortingLayerName = "default";
		
		return returnVal;
	}
	
	public void freeEffect(FXBase effect)
	{
		ObjectPoolManager.DestroyPooled(effect.gameObject);
	}
	
	private void loadEffectsFromResources()
	{
	
		m_hit01			= Resources.Load<FXHit>		        ("FX/Hit01");
        m_fire01        = Resources.Load<FXFire01>          ("FX/FXFire01");
        m_ice01         = Resources.Load<FXIce01>           ("FX/FXIce01");
        m_shock01       = Resources.Load<FXShock01>         ("FX/FXShock01");
		
	}
	
}
