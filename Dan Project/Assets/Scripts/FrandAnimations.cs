using UnityEngine;
using System.Collections;

public class FrandAnimations : MonoBehaviour {

CollectingFrands cf;
public bool idle;
public bool spooped;
public bool running; 
public bool withCorgi;
public float detectRange;
public float detectAngle;
GameObject corgiSprite;
GameObject corgi;
CorgiAnimations ca;


	// Use this for initialization
	void Start () {
	idle = false;
	spooped = true;
	running = false;
	withCorgi = false; 

	cf = GetComponentInParent<CollectingFrands>();
	corgiSprite = GameObject.Find("Corgi");
	ca = corgiSprite.GetComponent<CorgiAnimations>(); 
	corgi = GameObject.Find("Player 1");


	
	}
	
	// Update is called once per frame
	void Update () {

		animationHandler();
		stateDetector();
		withMyFrand();
	
	}

	void animationHandler()
	{
		GetComponent<Animator>().SetBool("Idle", idle);
		GetComponent<Animator>().SetBool("Spooped", spooped);
		GetComponent<Animator>().SetBool("Running", running);
	}

	void stateDetector()
	{
		if (cf.found == false)
		{
			spooped = true;
			idle = false;
			running = false; 
		}

		if (cf.found == true)
		{
			if (withCorgi == false)
			{
				spooped = false;
				idle = false;
				running = true;
			}

			if (withCorgi == true)
			{
				if (ca.spooped == true)
				{
					spooped = true;
					idle = false;
					running = false;
				}

				if (ca.idle == true)
				{
					spooped = false;
					idle = true;
					running = false;
				}

				if (ca.moving == true)
				{
					spooped = false;
					idle = false;
					running = true;
				}

			}
		}

	}

	void withMyFrand ()
	{
		Vector3 targetDirection = corgi.transform.position - this.transform.position; //gets direction of target 
		float targetAngle = Vector3.Angle(targetDirection,this.transform.forward); //gets angle to target

			if(Vector3.Distance(corgi.transform.position, this.transform.position) < detectRange && targetAngle < detectAngle){ //checks if target is in line of sight
				withCorgi = true;

			}
			else
			{
				withCorgi = false;
			}
	}
}