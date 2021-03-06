﻿using UnityEngine;
using System.Collections;

public class CorgiAnimations : MonoBehaviour {
public bool idle;
public bool moving;
public bool spooped;
public bool barking;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	stateDetector();
	animationHandler();
	}

	void stateDetector()
	{
		if (Input.GetButton("Move") ==true)
		{
			idle = false;
			spooped = false;
			moving = true;
		}

		if (!Input.GetButton("Move") ==true && spooped == false)
		{
			idle = true;
			moving = false;
		}

		if (!Input.GetButton("Move") ==true && spooped == true)
		{
			idle = false;
			moving = false;
		}

	}

	void animationHandler ()
	{
		GetComponent<Animator>().SetBool("Idle", idle);
		GetComponent<Animator>().SetBool("Moving", moving);
		GetComponent<Animator>().SetBool("Spooped", spooped); 
		GetComponent<Animator>().SetBool("Barking", barking);
	}
}
