using UnityEngine;
using System.Collections;

public class InMenuHUD : MonoBehaviour {

	public GameObject			startButton;


	
	public void init()
	{
		//startButton.SetActive(false);
	}

	public void SetActive(bool isActive)
	{
		startButton.SetActive(isActive);
	}

	public void showStartButton(bool showButton)
	{
		startButton.SetActive (showButton);
	}

	public bool isStartButtonVisible()
	{
		return startButton.activeInHierarchy;
	}

    public void pressStartButton()
    {
        Debug.Log("Start button pressed");
    }




}
