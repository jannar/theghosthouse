using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DogBeenSpooped : MonoBehaviour {

GameObject spoopMeter;
SpoopMeterScript sm;

public int numberOfGhostsInRange; 
public List<Transform> visibleGhosts = new List<Transform>(); 
public float spoopAmount = 0;
public float viewRadius; 
public float viewAngle;
public LayerMask obstacleMask; //layer mask for obstacles
public LayerMask targetMask; 
public bool spooped;
GameObject frandCounter;
FrandCounter fc;



	// Use this for initialization
	void Start () {
		frandCounter = GameObject.Find("FrandCounter");
		fc = frandCounter.GetComponent<FrandCounter>();
		spoopMeter = GameObject.Find("Spoop Meter");
		sm = spoopMeter.GetComponent<SpoopMeterScript>();
		spooped = false;

	}
	
	// Update is called once per frame
	void Update () {

		findGhosts ();
		amISpooped (); 
		numberOfGhostsInRange = visibleGhosts.Count;
	
	}

	void findGhosts()
	{
		visibleGhosts.Clear ();//clears the visible targets list so that targets no longer in fov will not remain in list
		Collider[] ghostsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask); 
		//gets array of all colliders on targets touching or within our view radius using a sphere.
		//to ensure they are targets we use our target layermask 
		for(int i = 0; i < ghostsInViewRadius.Length; i++)
		{ //runs a for loop on each of these colliders
			Transform target = ghostsInViewRadius [i].transform; //gets the transform of the target
			Vector3 dirToTarget = (target.position - transform.position).normalized; //check if target is within field of view angle
			//finds the direction to the target by taking it's position, subtracting our position, normalized
			if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{ //looks to see if the distance between
				//our forward vector and the direction to target is less than half of our view angle
				float distanceToTarget = Vector3.Distance (transform.position, target.position); //gets the distance between our position and the target's
				if(!Physics.Raycast (transform.position, dirToTarget, distanceToTarget, obstacleMask))
				{ //if there is no collision when performing a raycast:
					//when raycast returns false from our position in the direction of the target with the correct distance and layer
					visibleGhosts.Add (target);//adds target to visible targets list 

				}
			}
		}
	}

	void amISpooped()
	{
		if (numberOfGhostsInRange >=1)
		{
			spooped = true;
			sm.increaseMeter(numberOfGhostsInRange - fc.numOfFrands);  
		}

		if ((numberOfGhostsInRange - fc.numOfFrands) <= 0) 
		{
			spooped = false;
			sm.currentSpoopLevel--;
		}

	}
}
