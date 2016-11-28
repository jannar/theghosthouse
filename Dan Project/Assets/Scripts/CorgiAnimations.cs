using UnityEngine;
using System.Collections;

public class CorgiAnimations : MonoBehaviour {

public bool moving;
public bool barking;
public bool spooped;


	// Use this for initialization
	void Start () {

	moving = false; 
	barking = false;
	spooped = false; 
	
	}

	// Update is called once per frame
	void Update () {

	stateDetector(); 
	animationHandler();
	
	}

	void stateDetector () 
	{
		if (Input.GetButton("Move"))
		{
			moving = true;
		}
		else 
		{
			moving = false;
		}
	}

	void animationHandler ()
	{
		GetComponent<Animator>().SetBool("Moving", moving);
		GetComponent<Animator>().SetBool("Barking", barking);
		GetComponent<Animator>().SetBool("Spooped", spooped);
	}
}
