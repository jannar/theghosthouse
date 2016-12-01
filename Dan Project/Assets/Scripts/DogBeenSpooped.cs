using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DogBeenSpooped : MonoBehaviour {

	SpoopScript ss;
	public List<Collider> visibleGhosts = new List<Collider>(); 
	public float spoopAmount = 0;
	public float viewRadius; 
	public int numberOfGhostsinRange;
	public bool BeingSpooped;
	public LayerMask targetMask; 
	public Image healthBar;

	// Use this for initialization
	void Start () {

		ss = this.GetComponent<SpoopScript> (); 
		BeingSpooped = false;

	
	}
	
	// Update is called once per frame
	void Update () {

		//here's what goes here:
		//Look for ghosts...with the enemy script prob? Field of view?
		//If in range of ghost, BeingSpooped = true;

		SpoopUp ();
		//findGhosts ();
		//numberOfGhostsinRange = visibleGhosts.Count;
	
	}

	void SpoopUp(){


		if (BeingSpooped == true) {
			spoopAmount++;
		}

		if (BeingSpooped == false) {
			spoopAmount--;

			if (spoopAmount <= 0) {
				spoopAmount = 0;
			}
		}
		//fill in health bar here
		//spoopAmount++;
		
	}

//	void findGhosts()
//	{
//			visibleGhosts.Clear ();//clears the visible targets list so that targets no longer in fov will not remain in list
//			Collider[] ghostsInRange = Physics.OverlapSphere(transform.position, viewRadius, targetMask); 
//			//gets array of all colliders on targets touching or within our view radius using a sphere.
//			//to ensure they are targets we use our target layermask 
//			for(int i = 0; i < ghostsInRange.Length; i++){ //runs a for loop on each of these colliders
//			Collider target = ghostsInRange [i]; //gets the transform of the target
//			visibleGhosts.Add (target);//adds target to visible targets list 
//
//			Debug.Log (visibleGhosts.Count);
//							}
////			}
//		}
}
