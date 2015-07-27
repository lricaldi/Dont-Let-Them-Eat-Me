using UnityEngine;
using System.Collections;

public class UIMenuScript : MonoBehaviour {


    public void pressStartButton()
    {
        Debug.Log("Start button pressed");
        Application.LoadLevel("Game");
    }

}
