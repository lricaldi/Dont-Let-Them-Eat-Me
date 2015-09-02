using UnityEngine;
using System.Collections;

public class InMenuHUD : MonoBehaviour {

	public GameObject			startButton;


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





}
