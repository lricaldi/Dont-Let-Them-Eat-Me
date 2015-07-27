using UnityEngine;
using System.Collections;

public class ScreenFade : MonoBehaviour {

	public static ScreenFade		instance;
	
	private GameObject 		fadeImage;
	private float			fadeTime;
	private bool 			showing = true;
	private bool 			isFading = false;
	private float			fadeAlpha = 1f;
	private float			fadeSpeed = 0.01f;
	
	
	void Awake()
	{
		fadeImage = GameObject.Find("FadeImage");
		
		if(instance)
		{
			Destroy (gameObject);
			hide(0f, false);
			return;
		}

		instance = this;
		fadeAlpha = 1f;
		fadeImage.SetActive(true);
		fadeImage.GetComponent<CanvasRenderer>().SetAlpha(fadeAlpha);
		DontDestroyOnLoad(this);
	}
	
	void Update()
	{
		if(isFading && showing )
		{
			if(fadeAlpha < 0.99f)
			{
				fadeAlpha = Mathf.Lerp(fadeAlpha, 1f, Time.deltaTime *fadeSpeed);
				fadeImage.GetComponent<CanvasRenderer>().SetAlpha(fadeAlpha);
			}
			else
			{
				isFading = false;
				fadeAlpha = 1f;
				fadeImage.GetComponent<CanvasRenderer>().SetAlpha(fadeAlpha);
			}
			
		}
		
		if(isFading && !showing )
		{
			if(fadeAlpha > 0.01f)
			{
				fadeAlpha = Mathf.Lerp(fadeAlpha, 0f, Time.deltaTime * fadeSpeed);
				fadeImage.GetComponent<CanvasRenderer>().SetAlpha(fadeAlpha);
			}
			else
			{
				isFading = false;
				fadeAlpha = 0f;
				fadeImage.GetComponent<CanvasRenderer>().SetAlpha(fadeAlpha);
				instance.fadeImage.SetActive(false);
			}
		}
	}
	
	public bool isBusyFading()
	{
		return isFading;
	}
	
	public void show(float speed, bool forceFade)
	{
		fadeSpeed = speed;

		if(speed > 0f)
		{
			if(isFading && !forceFade)
			{
				return;
			}
			
			instance.fadeImage.SetActive(true);
			fadeImage.GetComponent<CanvasRenderer>().SetAlpha(0f);
			showing = true;
			isFading = true;
		}
		else
		{
			instance.fadeImage.SetActive(true);
			fadeAlpha = 1f;
			showing = true;
			isFading = false;
			fadeImage.GetComponent<CanvasRenderer>().SetAlpha(fadeAlpha);
		}
		
		
	}

	public void hide(float speed, bool forceFade)
	{
		fadeSpeed = speed;

		if(speed > 0f)
		{
			if(isFading && !forceFade)
			{
				return;
			}
			showing = false;
			isFading = true;
		}
		else
		{
			instance.fadeImage.SetActive(false);
			fadeAlpha = 0f;
			showing = false;
			isFading = false;
			fadeImage.GetComponent<CanvasRenderer>().SetAlpha(fadeAlpha);
		}
		
	}

	public float getAlpha()
	{
		return fadeAlpha;
	}
}
