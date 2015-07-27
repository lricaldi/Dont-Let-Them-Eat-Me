using UnityEngine;
using System.Collections;

public class EffectsManager : MonoBehaviour 
{
	private FXHit				m_hit01;
	private FXDust01			m_dust01;
	private FXDust02			m_dust02;
	private FXPlayerJumpDust	m_pJumpDust;
	private FXExplosion01		m_exp01;
    private FXElectricity01		m_electricity01;
	
	public enum FXType {FXT_Hit01, FXT_Dust01, FXT_Dust02, FXT_PJumpDust, FXT_Expl01, FXT_EnShot01, FXT_Electricity01, FXT_Length}
	
	public static EffectsManager instance = null;
	
	
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != null)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
		
	}
	void Start()
	{
		loadEffectsFromResources();
	}
	
	void Update()
	{
		
	}
	public FXBase getEffect(Vector2 position, Quaternion rotQuat, FXType type)
	{
		FXBase returnVal = null;
		switch(type)
		{
		case FXType.FXT_Hit01:
			FXHit effecth01 = ObjectPoolManager.CreatePooled( m_hit01.gameObject, position, rotQuat ).GetComponent<FXHit>();		
			effecth01.GetComponent<SpriteRenderer>().sortingOrder     = 0;
			effecth01.GetComponent<SpriteRenderer>().sortingLayerName   = "frontEffects";
			returnVal = effecth01;
			break;
		case FXType.FXT_Dust01:
			FXDust01 effectd01 = ObjectPoolManager.CreatePooled( m_dust01.gameObject, position, rotQuat ).GetComponent<FXDust01>();
            effectd01.GetComponent<SpriteRenderer>().sortingOrder = 0;
			effectd01.GetComponent<SpriteRenderer>().sortingLayerName   = "player";
			returnVal = effectd01;
			break;
		case FXType.FXT_Dust02:
			FXDust02 effectd02 = ObjectPoolManager.CreatePooled( m_dust02.gameObject, position, rotQuat ).GetComponent<FXDust02>();
            effectd02.GetComponent<SpriteRenderer>().sortingOrder = 0;
			effectd02.GetComponent<SpriteRenderer>().sortingLayerName   = "player";
			returnVal = effectd02;
			break;
		case FXType.FXT_PJumpDust:
			FXPlayerJumpDust effectpjd = ObjectPoolManager.CreatePooled( m_pJumpDust.gameObject, position, rotQuat ).GetComponent<FXPlayerJumpDust>();		
			returnVal = effectpjd;
			break;
		case FXType.FXT_Expl01:
			FXExplosion01 effectex01 = ObjectPoolManager.CreatePooled( m_exp01.gameObject, position, rotQuat ).GetComponent<FXExplosion01>();
            effectex01.GetComponent<SpriteRenderer>().sortingOrder = 0;
			effectex01.GetComponent<SpriteRenderer>().sortingLayerName  = "background01";
			returnVal = effectex01;
			break;
        
		case FXType.FXT_Electricity01:
			FXElectricity01 effectElect01 = ObjectPoolManager.CreatePooled(m_electricity01.gameObject, position, rotQuat).GetComponent<FXElectricity01>();
            effectElect01.GetComponent<SpriteRenderer>().sortingOrder = 0;
			effectElect01.GetComponent<SpriteRenderer>().sortingLayerName = "frontEffects";
			returnVal = effectElect01;
			break;
			
		
			
		}
		return returnVal;
	}
	
	public void freeEffect(FXBase effect)
	{
		ObjectPoolManager.DestroyPooled(effect.gameObject);
	}
	
	private void loadEffectsFromResources()
	{
	
		m_hit01			= Resources.Load<FXHit>		        ("FXs/Hit01");
		m_dust01 		= Resources.Load<FXDust01>	        ("FXs/Dust01");
		m_dust02 		= Resources.Load<FXDust02>	        ("FXs/Dust02");
		m_pJumpDust 	= Resources.Load<FXPlayerJumpDust>	("FXs/PlayerDustJumpEffect");
		m_exp01 		= Resources.Load<FXExplosion01>	    ("FXs/Explosion01");
        m_electricity01 = Resources.Load<FXElectricity01>   ("FXs/Electricity01");
	}
	
}
