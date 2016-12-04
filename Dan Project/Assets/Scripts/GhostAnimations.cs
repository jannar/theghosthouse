using UnityEngine;
using System.Collections;

public class GhostAnimations : MonoBehaviour {

public bool chasing;
public bool stunned;
public bool idle;
public bool fleeing;
public bool spooping; 
Enemy enemy;
SpriteRenderer sr;
GameObject corgi;
SpoopScript ss;
GhostEvac ge;
//NavMeshAgent nma;





	// Use this for initialization
	void Start () {

	fleeing = false; 
	chasing = false;
	stunned = false; 
	idle = false;
	spooping = false;

	enemy = GetComponentInParent<Enemy>();
	sr = GetComponent<SpriteRenderer>(); 
	ss = GetComponentInParent<SpoopScript>();
	ge = GetComponentInParent<GhostEvac>();
	//nma.GetComponentInParent<NavMeshAgent>();
	corgi = GameObject.Find("Player 1");

	
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

		if (ss.spoopingTheDog == true)
		{
			spooping = true;
		}

		if (ss.spoopingTheDog == false)
		{
			spooping = false; 
		}

		if (ge.inLineSight ==true)
		{
			stunned = true;
			//nma.speed = 0;
		}

		if (ge.inLineSight == false)
		{
			stunned = false;
			//nma.speed = ge.originalSpeed;
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
		GetComponent<Animator>().SetBool("Idle", idle);
		GetComponent<Animator>().SetBool("Chasing", chasing);
		GetComponent<Animator>().SetBool("Stunned", stunned);
		GetComponent<Animator>().SetBool("Fleeing", fleeing);
		GetComponent<Animator>().SetBool("Spooping", spooping); 
	}
}
