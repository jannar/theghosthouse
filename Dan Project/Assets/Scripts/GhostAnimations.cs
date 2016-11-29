using UnityEngine;
using System.Collections;

public class GhostAnimations : MonoBehaviour {

public bool chasing;
public bool stunned;
public bool idle;
public bool fleeing;
Enemy enemy;
SpriteRenderer sr;
GameObject corgi;





	// Use this for initialization
	void Start () {

	fleeing = false; 
	chasing = false;
	stunned = false; 
	idle = false;

	enemy = GetComponentInParent<Enemy>();
	sr = GetComponent<SpriteRenderer>(); 
	corgi = GameObject.Find("Player");

	
	}

	// Update is called once per frame
	void Update () {

	stateDetector(); 
	animationHandler();
	lookAtMe();
	
	}

	void stateDetector () 
	{
		if (enemy.movementActive == false)
		{
			idle = true;
			chasing = false;
		}

		if (enemy.movementActive == true)
		{
			idle = false;
			chasing = true;
		}
	}

	void lookAtMe()
	{
		if (transform.parent.position.x >= corgi.transform.position.x)
		{
			sr.flipX = true;
		}
		else 
		{
			sr.flipX = false;
		}
	}

	void animationHandler ()
	{
		GetComponent<Animator>().SetBool("idle", idle);
		GetComponent<Animator>().SetBool("Chasing", chasing);
		GetComponent<Animator>().SetBool("Stunned", stunned);
		GetComponent<Animator>().SetBool("Fleeing", fleeing);
	}
}
