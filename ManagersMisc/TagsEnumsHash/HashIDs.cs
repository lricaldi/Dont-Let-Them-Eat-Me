using UnityEngine;
using System.Collections;


public class HashIDs : MonoBehaviour {


    //Common
    [HideInInspector]
    public int dead;


	// Target
    [HideInInspector]
    public int happy;
    [HideInInspector]
    public int attacked;
    [HideInInspector]
    public int ok;
    [HideInInspector]
    public int sad;
    [HideInInspector]
    public int won;


    [HideInInspector]
    public int idle;
    [HideInInspector]
    public int move;
    [HideInInspector]
    public int eat;
    

	//public int hashName; 
	




	public void Awake ()
	{
        //Debug.Log("HashIds init");

        happy       = Animator.StringToHash("happy");
        attacked    = Animator.StringToHash("attacked");
        ok          = Animator.StringToHash("ok");
        sad         = Animator.StringToHash("sad");
        dead        = Animator.StringToHash("dead");
        won         = Animator.StringToHash("won");

        idle        = Animator.StringToHash("idle");
        move        = Animator.StringToHash("move");
        eat         = Animator.StringToHash("eat");
		
	}
}
